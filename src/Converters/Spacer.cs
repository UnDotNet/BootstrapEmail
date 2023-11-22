namespace UnDotNet.BootstrapEmail.Converters;

internal class Spacer(IHtmlDocument doc) : Base<Spacer>(doc)
{
    public override void Build()
    {
        EachNode("*[class*=s-]", node =>
        {
            node.ClassList.Add("w-full");
            node.Replace(TemplateNode("table", Doc, node.ClassName, "&nbsp;"));
        });
    }
}
