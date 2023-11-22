using System.Reflection;
using AngleSharp;
using AngleSharp.XPath;
using UnDotNet.HtmlToText;
using UnDotNet.BootstrapEmail.Converters;

namespace UnDotNet.BootstrapEmail;

// ReSharper disable NotAccessedPositionalProperty.Global
public record MultipartResult(string Text, string Html);

public class BootstrapCompiler
{
    private string Type { get; }
    private IHtmlDocument Doc { get; set; }
    private string RawInput { get; }

    public BootstrapCompiler(string input, string type = "string")
    {
        Type = type;
        RawInput = input;
        var html = Type switch
        {
            "string" => input,
            "file" => File.ReadAllText(input),
            _ => ""
        };
        html = AddLayout(html);
        Doc = new HtmlParser().ParseDocument(html);
        SassLoadPaths();
    }

    /// <summary>
    /// Compiles provided Html to Text using UnDotNet.HtmlToText
    /// </summary>
    /// <returns>text version of HTML</returns>
    // ReSharper disable once MemberCanBePrivate.Global
    public string Text()
    {
        var text = RawInput;
        text = Regex.Replace(text, "<preview>.*</preview>", "");
        text = text.Trim();
        return new HtmlToTextConverter().Convert(text);
    }

    public string Html()
    {
        CompileHtml();
        InlineCss();
        ConfigureHtml();
        return FinalizeDocument();
    }

    public MultipartResult Multipart()
    {
        return new MultipartResult(
            Html: Html(),
            Text: Text()
        );
    }

    private void InlineCss()
    {
        BuildPremailerDoc();
    }

    private string AddLayout(string html)
    {
        return Layout.Replace(html);
    }

    private void SassLoadPaths()
    {
        var assembly = typeof(Badge).GetTypeInfo().Assembly;
        using var stream = assembly.GetManifestResourceStream($"UnDotNet.BootstrapEmail.Templates.bootstrap-head.css");
        if (stream is null)
            throw new InvalidOperationException("Embedded resource bootstrap-head.css was not found!");
        using var reader = new StreamReader(stream);
        Constants.SassCacheHead = reader.ReadToEnd();

        using var stream2 =
            assembly.GetManifestResourceStream($"UnDotNet.BootstrapEmail.Templates.bootstrap-email.css");
        if (stream2 is null)
            throw new InvalidOperationException("Embedded resource bootstrap-email.css was not found!");
        using var reader2 = new StreamReader(stream2);
        Constants.SassCacheEmail = reader2.ReadToEnd();

        var styleBlocks = Doc.Head?.QuerySelectorAll("style");
        if (styleBlocks == null) return;
        foreach (var block in styleBlocks)
        {
            Constants.SassCacheEmail += "\n" + block.InnerHtml;
            block.Remove();
        }
    }

    internal void BuildPremailerDoc()
    {
        var html = Doc.ToHtml();
        var pm = new PreMailer.Net.PreMailer(html);
        var pmResult = pm.MoveCssInline(css: Constants.SassCacheEmail);
        var htmlAfter = new HtmlParser().ParseDocument(pmResult.Html);
        Doc = htmlAfter;
    }

    internal void CompileHtml()
    {
        new EnsureDocType(Doc).Build();
        new Body(Doc).Build();
        new Block(Doc).Build();

        new Button(Doc).Build();
        new Badge(Doc).Build();
        new Alert(Doc).Build();
        new Card(Doc).Build();
        new Hr(Doc).Build();
        new Container(Doc).Build();
        new Grid(Doc).Build();
        new Stack(Doc).Build();

        new Color(Doc).Build();
        new Spacing(Doc).Build();
        new Margin(Doc).Build();
        new Spacer(Doc).Build();
        new Align(Doc).Build();
        new Padding(Doc).Build();

        new PreviewText(Doc).Build();
        new Table(Doc).Build();
        new VersionComment(Doc).Build();
    }


    internal void ConfigureHtml()
    {
        new AddMissingMetaTags(Doc).Build();
        new HeadStyle(Doc).Build();
        new ForceEncoding(Doc).Build();
    }

    // Premailer kills these for some reason, so we put them back.
    private void TidyTableAlignments()
    {
        var tables = new[] { "TABLE", "TR", "TD", "TH" };
        var nodes = Doc.Body.SelectNodes(".//*[contains(@style, 'text-align')]");
        foreach (var node in nodes)
        {
            if (node is not IHtmlElement el || !tables.Contains(node.NodeName)) continue;

            var styles = el.Attributes["style"]?.Value;
            if (styles is null) continue;

            styles = styles.ToLower();
            if (styles.Contains("text-align: left;"))
            {
                styles = styles.Replace("text-align: left;", "");
                el.SetAttribute("align", "left");
                el.SetAttribute("style", styles);
            }

            if (styles.Contains("text-align: center;"))
            {
                styles = styles.Replace("text-align: center;", "");
                el.SetAttribute("align", "center");
                el.SetAttribute("style", styles);
            }
        }
    }

    internal string FinalizeDocument()
    {
        TidyTableAlignments();
        var html = Doc.ToHtml();
        html = SupportUrlTokens.Replace(html);
        html = EnsureDocType.Replace(html);
        // html = ForceEncoding.Replace(html);
        html = BeautifyHtml.Replace(html);
        // trailing new line
        return html.Trim() + "\n";
    }
}
