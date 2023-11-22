namespace UnDotNet.BootstrapEmail.Converters;

internal class Alert(IHtmlDocument doc) : Base<Alert>(doc)
{
    public override void Build()
    {
        EachNode(".alert", node =>
        {
            var classes = node.ClassName ?? "";
            node.Attributes.RemoveNamedItem("class");
            node.Replace(TemplateNode("table", Doc, classes, node.OuterHtml));
        });
    }
}
