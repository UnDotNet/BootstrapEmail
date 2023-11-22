namespace UnDotNet.BootstrapEmail.Converters;

internal class PreviewText : Base<PreviewText>
{
    private readonly IHtmlDocument _doc;

    public PreviewText(IHtmlDocument doc) : base(doc)
    {
        _doc = doc;
    }

    public override void Build()
    {
        var previewNode = _doc.QuerySelector("preview");
        if (previewNode == null)
        {
            return;
        }

        // https://www.litmus.com/blog/the-little-known-preview-text-hack-you-may-want-to-use-in-every-email/
        // apply spacing after the text max of 278 characters so it doesn't show body text
        // update to newest info at https://parcel.io/blog/preheader-spacing

        var neededSpaces = Math.Max(278 - previewNode.TextContent.Length, 0);
        var extraSpace = string.Concat(Enumerable.Repeat("&#847; &zwnj; &nbsp; &#8199; &shy; ", neededSpaces));

        previewNode.InnerHtml += extraSpace;

        var html = Template("div", classes: "preview", contents: previewNode.TextContent);

        previewNode.Remove();

        _doc.Body?.Insert(AdjacentPosition.AfterBegin, html);
    }
}
