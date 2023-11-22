namespace UnDotNet.BootstrapEmail.Converters;

internal class Hr(IHtmlDocument doc) : Base<Hr>(doc)
{
    public override void Build()
    {
        EachNode("hr", node =>
        {
            var margin = Margin(node) ? "" : "my-5";
            node.Replace(TemplateNode("table", Doc, $"{margin} hr {node.ClassName ?? ""}", ""));
        });
    }
}
