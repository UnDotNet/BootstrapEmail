namespace UnDotNet.BootstrapEmail.Converters;

internal class VersionComment : Base<VersionComment>
{
    private readonly IHtmlDocument _doc;

    public VersionComment(IHtmlDocument doc) : base(doc)
    {
        _doc = doc;
    }

    public override void Build()
    {
        _doc.Head?.Insert(AdjacentPosition.BeforeEnd, GetVersion());
    }

    private string GetVersion()
    {
        return $"  <!-- Compiled with Bootstrap Email DotNet version: {Constants.Version} -->\n";
    }
}
