// ReSharper disable StringLiteralTypo
namespace UnDotNet.BootstrapEmail.Converters;

internal class Table(IHtmlDocument doc) : Base<Table>(doc)
{
    public override void Build()
    {
        EachNode("table", node =>
        {
            node.SetAttribute("border", "0");
            node.SetAttribute("cellpadding", "0");
            node.SetAttribute("cellspacing", "0");
        });
    }
}
