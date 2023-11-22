namespace UnDotNet.BootstrapEmail.Converters;

internal class Body : Base<Body>
{
    private readonly IHtmlDocument _doc;

    public Body(IHtmlDocument doc) : base(doc)
    {
        _doc = doc;
    }

    public override void Build()
    {
        if (_doc.Body != null)
            _doc.Body.InnerHtml = Template("body", $"{_doc.Body.ClassName} body", _doc.Body.InnerHtml);
    }
}
