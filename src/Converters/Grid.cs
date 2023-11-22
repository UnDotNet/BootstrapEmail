using AngleSharp.XPath;

namespace UnDotNet.BootstrapEmail.Converters;

internal class Grid(IHtmlDocument doc) : Base<Grid>(doc)
{
    public override void Build()
    {
        EachNode(".row", node =>
        {
            if (node.SelectNodes("./*[contains(@class, 'col-lg-')]").Count > 0)
            {
                AddClass(node, "row-responsive");
            }

            var tableHtml = "";


            var cols = node.SelectNodes("./*[contains(@class, 'col')]");
            foreach (var col in cols)
            {
                if (col is not IElement el) continue;
                var replace2 = Template("td", el.ClassName ?? "", el.InnerHtml);
                tableHtml += replace2;
            }

            var tableNode = Template("table-to-tr", "", tableHtml);
            var replace = TemplateNode("div", Doc, node.ClassName, tableNode)[0];

            node.Parent?.InsertBefore(replace, node);
            node.RemoveFromParent();
        });
    }
}
