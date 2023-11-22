namespace UnDotNet.BootstrapEmail.Converters;

internal class Paragraph(IHtmlDocument doc) : Base<Paragraph>(doc)
{
    public override void Build()
    {
        EachNode("p", node =>
        {
            if (!Margin(node))
            {
                AddClass(node, "mb-4");
            }
        });
    }
}
