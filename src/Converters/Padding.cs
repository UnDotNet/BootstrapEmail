namespace UnDotNet.BootstrapEmail.Converters;

internal class Padding(IHtmlDocument doc) : Base<Padding>(doc)
{
    public override void Build()
    {
        var selector =
            "*[class^=p-], *[class^=pt-], *[class^=pr-], *[class^=pb-], *[class^=pl-], *[class^=px-], *[class^=py-], *[class*=' p-'], *[class*=' pt-'], *[class*=' pr-'], *[class*=' pb-'], *[class*=' pl-'], *[class*=' px-'], *[class*=' py-']";

        EachNode(selector, node =>
        {
            if (new[] { "table", "td", "a" }.Contains(node.LocalName)) return;

            if (node.ClassName is null) return;

            var paddingRegex = new Regex(@"(p[trblxy]?-(lg-)?\d+)");
            //TODO: refactor this
            var classes = paddingRegex.Replace(node.ClassName, "").Split(' ').Where(c => !string.IsNullOrWhiteSpace(c))
                .ToArray().Join(' ');

            node.ClassName = paddingRegex.Replace(node.ClassName, "").Trim();
            node.Replace(TemplateNode("table", Doc, classes, node.OuterHtml));
        });
    }
}
