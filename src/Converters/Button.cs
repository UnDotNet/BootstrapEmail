namespace UnDotNet.BootstrapEmail.Converters;

internal class Button : Base<Button>
{
    public Button(IHtmlDocument doc) : base(doc) { }

    public override void Build()
    {
        EachNode(".btn", node =>
        {
            var classes = node.ClassName;
            node.Attributes.RemoveNamedItem("class");

            var contents = node.OuterHtml;
            node.Replace(TemplateNode("table", Doc, classes, contents));
        });
    }
}
