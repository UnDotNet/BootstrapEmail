namespace UnDotNet.BootstrapEmail.Converters;

internal class Stack(IHtmlDocument doc) : Base<Stack>(doc)
{
    public override void Build()
    {
        StackRow();
        StackCol();
    }

    private void StackRow()
    {
        EachNode(".stack-row", node =>
        {
            var html = "";
            foreach (var child in node.Children)
            {
                html += Template("td", "stack-cell", contents: child.OuterHtml);
            }

            node.Replace(TemplateNode("table-to-tr", Doc, node.ClassName, html));
        });
    }

    private void StackCol()
    {
        EachNode(".stack-col", node =>
        {
            var html = "";
            foreach (var child in node.Children)
            {
                html += Template("tr", "stack-cell", contents: child.OuterHtml);
            }

            node.Replace(TemplateNode("table-to-tbody", Doc, node.ClassName, html));
            // node.OuterHtml = Template("table-to-tbody", node.ClassName, html);
        });
    }
}
