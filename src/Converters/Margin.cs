namespace UnDotNet.BootstrapEmail.Converters;

internal class Margin(IHtmlDocument doc) : Base<Margin>(doc)
{
    public override void Build()
    {
        var selector =
            "*[class^='my-'], *[class^='mt-'], *[class^='mb-'], *[class*=' my-'], *[class*=' mt-'], *[class*=' mb-']";

        EachNode(selector, node =>
        {
            if (node.ClassName is null) return;
            var classes = node.ClassName;
            var topClass = Regex.Match(classes, @"m[ty]{1}-(lg-)?(\d+)").Value;
            var bottomClass = Regex.Match(classes, @"m[by]{1}-(lg-)?(\d+)").Value;
            if (!string.IsNullOrEmpty(topClass))
            {
                node.ClassName = node.ClassName.Replace(topClass, "");
            }

            if (!string.IsNullOrEmpty(bottomClass))
            {
                node.ClassName = node.ClassName.Replace(bottomClass, "");
            }

            node.ClassName = node.ClassName.Trim();

            if (!string.IsNullOrEmpty(topClass))
            {
                var newClass = topClass.Replace("mt-", "").Replace("my-", "");
                newClass = $"s-{newClass}";
                node.InsertBefore(TemplateNode("div", Doc, newClass, "&nbsp;"));
            }

            if (!string.IsNullOrEmpty(bottomClass))
            {
                var newClass = bottomClass.Replace("mb-", "").Replace("my-", "");
                newClass = $"s-{newClass}";
                node.InsertAfter(TemplateNode("div", Doc, newClass, "&nbsp;"));
            }
        });
    }
}
