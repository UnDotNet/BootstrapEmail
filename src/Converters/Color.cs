namespace UnDotNet.BootstrapEmail.Converters;

internal class Color(IHtmlDocument doc) : Base<Color>(doc)
{
    public override void Build()
    {
        EachNode("*[class*=bg-]", node =>
        {
            // only do automatic thing for div
            if (node.TagName != "DIV") return;

            var classes = node.ClassName;
            node.Attributes.RemoveNamedItem("class");

            var contents = node.InnerHtml;
            node.Replace(TemplateNode("table", Doc, $"{classes} w-full", contents));
        });

        EachNode("*[class*=dark]", node =>
        {
            var classes = node.ClassName;
            foreach (var className in classes.Split(" "))
            {
                if (!className.StartsWith("dark:")) continue;
                var color = className.Replace("dark:", "dark-");
                node.ClassList.Remove(className);
                node.ClassList.Add(color);
            }
        });
    }
}
