namespace UnDotNet.BootstrapEmail.Converters;

internal class Layout(IHtmlDocument doc) : Base<Layout>(doc)
{
    public static string Replace(string html)
    {
        var doc = new HtmlParser().ParseDocument(html);
        if (!string.IsNullOrWhiteSpace(doc.Head?.InnerHtml)) return html;

        return Template("layout", "", doc.Body?.OuterHtml ?? "");
    }
}
