namespace UnDotNet.BootstrapEmail.Converters;

internal class Align : Base<Align>
{
    private readonly IHtmlDocument _doc;

    public Align(IHtmlDocument doc) : base(doc)
    {
        _doc = doc;
    }

    public override void Build()
    {
        string[] types = { "left", "center", "right" };
        foreach (var type in types)
        {
            var fullType = "ax-" + type;
            foreach (var node in _doc.QuerySelectorAll("." + fullType))
            {
                AlignHelper(node, fullType, type);
            }
        }
    }

    private void AlignHelper(IElement node, string fullType, string type)
    {
        if (!Table(node) && !Td(node))
        {
            node.ClassList.Remove(fullType);
            var nodes = TemplateNode("table", Doc, fullType, node.OuterHtml);
            (nodes[0] as IElement)?.SetAttribute("align", type);
            node.ReplaceWith(nodes);
        }
        else
        {
            node.SetAttribute("align", type);
        }
    }
}
