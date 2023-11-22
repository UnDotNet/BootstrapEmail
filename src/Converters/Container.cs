namespace UnDotNet.BootstrapEmail.Converters;

internal class Container(IHtmlDocument doc) : Base<Container>(doc)
{
    public override void Build()
    {
        EachNode(".container",
            node => { node.Replace(TemplateNode("container", Doc, node.ClassName, node.InnerHtml)); });
        EachNode(".container-fluid",
            node => { node.Replace(TemplateNode("table", Doc, node.ClassName, node.InnerHtml)); });
    }
}
