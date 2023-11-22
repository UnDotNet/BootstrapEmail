namespace UnDotNet.BootstrapEmail.Converters;

internal class Block(IHtmlDocument doc) : Base<Block>(doc)
{
    public override void Build()
    {
        EachNode("block, .to-table", node =>
        {
            // add .to-table if it's not already there
            if (!node.ClassList.Contains("to-table"))
            {
                node.ClassList.Add("to-table");
            }

            node.Replace(TemplateNode("table", Doc, node.ClassName, node.InnerHtml));
        });
    }
}
