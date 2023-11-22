namespace UnDotNet.BootstrapEmail.Converters;

internal class Badge(IHtmlDocument doc) : Base<Badge>(doc)
{
    public override void Build()
    {
        EachNode(".badge", node =>
        {
            var classes = node.ClassName;
            node.Attributes.RemoveNamedItem("class");

            var contents = node.OuterHtml;
            node.Replace(TemplateNode("table-left", Doc, classes, contents));
        });
    }
}
