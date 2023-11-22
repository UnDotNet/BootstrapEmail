namespace UnDotNet.BootstrapEmail.Converters;

internal class Color(IHtmlDocument doc) : Base<Color>(doc)
{
    public override void Build()
    {
        EachNode("*[class*=bg-]", node =>
        {
            // only do automatic thing for div
            if (node.TagName != "div") return;

            node.ClassList.Add("w-full");

            var classes = node.ClassName;
            node.Attributes.RemoveNamedItem("class");

            var contents = node.InnerHtml;
            node.Replace(TemplateNode("table", Doc, classes, contents));
        });
    }
}
