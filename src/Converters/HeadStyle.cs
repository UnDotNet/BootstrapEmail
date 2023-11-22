using NUglify;

namespace UnDotNet.BootstrapEmail.Converters;

internal class HeadStyle : Base<HeadStyle>
{
    private readonly IHtmlDocument _doc;

    public HeadStyle(IHtmlDocument doc) : base(doc)
    {
        _doc = doc;
    }

    public override void Build()
    {
        _doc.Head?.AppendChild(BootstrapEmailHead());
    }

    private IElement BootstrapEmailHead()
    {
        var style = _doc.CreateElement("style");
        style.SetAttribute("type", "text/css");
        style.TextContent = PurgedCssFromHead();
        return style;
    }

    private string PurgedCssFromHead()
    {
        var css = Constants.SassCacheHead.Split("/*! allow_purge_after */");
        var custom = css.Length > 1 ? css[1] : "";
        var groups = Regex.Matches(custom, @"\w*\.[\w\-]*[\s\S\n]+?(?=})}{1}")
            .Select(m => m.Value);
        foreach (var group in groups)
        {
            var selectors = Regex.Matches(group, @"(\.[\w\-]*).*?((,+?)|{+?)")
                .Select(m => m.Groups[1].Value)
                .Distinct();
            var exist = selectors.Any(selector => _doc.QuerySelector(selector) != null);
            if (!exist)
            {
                custom = custom.Replace(group, "");
            }
        }

        var options = RegexOptions.Multiline;

        var regex = new Regex(@"\n\s*\n+", options);
        var result = regex.Replace(css[0] + custom, "\n");

        result = Uglify.Css(result).Code;

        return result;
    }
}
