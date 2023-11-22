using AngleSharp.XPath;

namespace UnDotNet.BootstrapEmail.Converters;

internal class Spacing(IHtmlDocument doc) : Base<Spacing>(doc)
{
    public override void Build()
    {
        // *:not(:last-child)
        // tbody > tr > td > :not(:last-child)
        // tbody > tr > td > * :not(:last-child)

        EachNode("*[class*=space-y-]", node =>
        {
            var spacerMatches = Regex.Match(node.ClassName ?? "", @"space-y-((lg-)?\d+)");
            var spacer = spacerMatches.Groups[0].Value;
            var spacerValue = spacerMatches.Groups[1].Value;
            // get all direct children except the first

            node.GetSelector();

            // var children = node.QuerySelectorAll($"{node.GetSelector()}>*:not(:last-child), {node.GetSelector()} > tbody > tr > td > * :not(:last-child)");
            var children = node.SelectNodes("./*[position() < last()] | ./tbody/tr/td/*[position() < last()]");
            foreach (var child in children)
            {
                if (child is not IElement el) continue;
                if (MarginBottom(el)) continue;
                el.ClassList.Add($"mb-{spacerValue}");
            }

            node.ClassList.Remove(spacer);
            if (node.ClassList.Length == 0) node.ClassName = null;
        });
    }
}
