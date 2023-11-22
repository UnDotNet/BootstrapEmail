using System.Reflection;

namespace UnDotNet.BootstrapEmail.Converters;

internal class Base<T>(IHtmlDocument doc) where T : Base<T>
{
    protected IHtmlDocument Doc { get; } = doc;

    // ReSharper disable once StaticMemberInGenericType
    private static readonly Dictionary<string, string> CachedTemplates = new();

    // public static void Build(IHtmlDocument doc)
    // {
    //     var instance = (T)Activator.CreateInstance(typeof(T), doc)!;
    //     instance.Build();
    // }

    public virtual void Build()
    {
        // implementation here
    }


    protected static string Template(string file, string classes, string contents)
    {
        if (!CachedTemplates.TryGetValue(file, out var template))
        {
            var assembly = typeof(Badge).GetTypeInfo().Assembly;
            using var stream = assembly.GetManifestResourceStream($"UnDotNet.BootstrapEmail.Templates.{file}.html");
            if (stream is null) throw new Exception($"BootstrapEmail template {file} not found");
            using var reader = new StreamReader(stream);
            template = reader.ReadToEnd().Trim();
            CachedTemplates[file] = template;
        }

        if (template is "") throw new Exception($"BootstrapEmail template {file} not found");

        classes = classes.Split().Join(" ");
        template = template.Replace("{{ classes }}", classes);
        template = template.Replace("{{ contents }}", contents);
        return template;
    }

    protected static INode[] TemplateNode(string file, IHtmlDocument doc, string? classes, string contents)
    {
        if (!CachedTemplates.TryGetValue(file, out var template))
        {
            var assembly = typeof(Badge).GetTypeInfo().Assembly;
            using var stream = assembly.GetManifestResourceStream($"UnDotNet.BootstrapEmail.Templates.{file}.html");
            if (stream is null) throw new Exception($"BootstrapEmail template {file} not found");
            using var reader = new StreamReader(stream);
            template = reader.ReadToEnd().Trim();
            CachedTemplates[file] = template;
        }

        if (template is "") throw new Exception($"BootstrapEmail template {file} not found");

        classes ??= "";
        classes = classes.Split().Join(" ");
        if (string.IsNullOrEmpty(contents)) contents = "";

        template = template.Replace("{{ classes }}", classes);
        template = template.Replace("{{ contents }}", contents);
        var parser = new HtmlParser();
        var fragment = parser.ParseFragment(template, doc.Body!).ToArray();
        return fragment;
    }


    protected void EachNode(string cssLookup, Action<IElement> action)
    {
        var nodes = Doc.QuerySelectorAll(cssLookup).Reverse(); //.OrderBy<>(n => n.Ancestors.Count()).Reverse();
        foreach (var node in nodes)
        {
            action(node);
        }
    }

    public void AddClass(IElement node, string className)
    {
        node.ClassList.Add(className);
        if (node.ClassName != null) node.ClassName = node.ClassName.RemoveMultipleSpaces().Trim();
    }

    protected bool Margin(IElement node)
    {
        return MarginTop(node) || MarginBottom(node);
    }

    private bool MarginTop(IElement node)
    {
        return Regex.IsMatch(node.ClassName ?? "", @"m[ty]{1}-(lg-)?\d+");
    }

    protected bool MarginBottom(IElement node)
    {
        return Regex.IsMatch(node.ClassName ?? "", @"m[by]{1}-(lg-)?\d+");
    }

    protected bool Table(IElement node)
    {
        return node.TagName == "TABLE";
    }

    protected bool Td(IElement node)
    {
        return node.TagName == "TD";
    }
}
