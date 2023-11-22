namespace UnDotNet.BootstrapEmail.Converters;

internal class AddMissingMetaTags : Base<AddMissingMetaTags>
{
    private static readonly MetaTag[] MetaTags = new[]
    {
        new MetaTag
        {
            Query = "meta[http-equiv=\"Content-Type\"]",
            Code = "<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\">"
        },
        new MetaTag
        {
            Query = "meta[http-equiv=\"x-ua-compatible\"]",
            Code = "<meta http-equiv=\"x-ua-compatible\" content=\"ie=edge\">"
        },
        new MetaTag
        {
            Query = "meta[name=\"x-apple-disable-message-reformatting\"]",
            Code = "<meta name=\"x-apple-disable-message-reformatting\">"
        },
        new MetaTag
        {
            Query = "meta[name=\"viewport\"]",
            Code = "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">"
        },
        new MetaTag
        {
            Query = "meta[name=\"format-detection\"]",
            Code = "<meta name=\"format-detection\" content=\"telephone=no, date=no, address=no, email=no\">"
        }
    }.Reverse().ToArray();

    private readonly IHtmlDocument _doc;
    public AddMissingMetaTags(IHtmlDocument doc) : base(doc)
    {
        _doc = doc;
    }

    public override void Build()
    {
        foreach (var tagHash in MetaTags)
        {
            var head = _doc.QuerySelector("head");
            if (head is null) continue;
            var result = head.QuerySelector(tagHash.Query);
            if (result is null)
            {
                // var newMeta = new HtmlParser().ParseFragment(tagHash.Code, doc.Body);
                // head.Prepend(newMeta[0]);

                // var newMeta = new HtmlParser().ParseHead(tagHash.Code);
                // newMeta.InnerHtml = tagHash.Code;
                head.Insert(AdjacentPosition.BeforeEnd, tagHash.Code);
            }
        }
    }
}

public class MetaTag
{
    public string Query { get; init; } = "";
    public string Code { get; init; } = "";
}
