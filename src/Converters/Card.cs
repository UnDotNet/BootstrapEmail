namespace UnDotNet.BootstrapEmail.Converters;

internal class Card(IHtmlDocument doc) : Base<Card>(doc)
{
    public override void Build()
    {
        EachNode(".card", node => { node.Replace(TemplateNode("table", Doc, node.ClassName, node.InnerHtml)); });
        EachNode(".card-body", node => { node.Replace(TemplateNode("table", Doc, node.ClassName, node.InnerHtml)); });
    }
}
