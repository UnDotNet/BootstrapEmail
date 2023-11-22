
namespace UnDotNet.BootstrapEmail.Tests.Converters;

[TestClass]
public class AddMissingMetaTagsTests
{
    [TestMethod]
    public void ShouldAddAllMissingMetaTags()
    {
        var html = """
                   <html>
                   <head>
                   </head>
                   </html>
                   """;
        var result = """
                     <head>
                       <meta name="format-detection" content="telephone=no, date=no, address=no, email=no">
                       <meta name="viewport" content="width=device-width, initial-scale=1">
                       <meta name="x-apple-disable-message-reformatting">
                       <meta http-equiv="x-ua-compatible" content="ie=edge">
                       <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
                     </head>
                     """;
        var parser = new HtmlParser();
        var doc = parser.ParseDocument(html);
        var converter = new AddMissingMetaTags(doc);
        converter.Build();
        doc.Head?.Prettify().ShouldBe(result);
    }

    [TestMethod]
    public void ShouldAddHeadIfMissing()
    {
        var html = """
                   <html>
                   </html>
                   """;
        var result = """
                     <html>
                       <head>
                         <meta name="format-detection" content="telephone=no, date=no, address=no, email=no">
                         <meta name="viewport" content="width=device-width, initial-scale=1">
                         <meta name="x-apple-disable-message-reformatting">
                         <meta http-equiv="x-ua-compatible" content="ie=edge">
                         <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
                       </head>
                     """;
        var parser = new HtmlParser();
        var doc = parser.ParseDocument(html);
        var converter = new AddMissingMetaTags(doc);
        converter.Build();
        doc.Prettify().ShouldStartWith(result);
    }
    
    [TestMethod]
    public void ShouldAddOnlyOneMissingMetaTag()
    {
        var html = """
                   <html>
                     <head>
                       <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
                       <meta http-equiv="x-ua-compatible" content="ie=edge">
                       <meta name="x-apple-disable-message-reformatting">
                       <meta name="viewport" content="width=device-width, initial-scale=1">
                     </head>
                     <body></body>
                   </html>
                   """;
        var result = """
                     <html>
                       <head>
                         <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
                         <meta http-equiv="x-ua-compatible" content="ie=edge">
                         <meta name="x-apple-disable-message-reformatting">
                         <meta name="viewport" content="width=device-width, initial-scale=1">
                         <meta name="format-detection" content="telephone=no, date=no, address=no, email=no">
                       </head>
                     """;
        var parser = new HtmlParser();
        var doc = parser.ParseDocument(html);
        var converter = new AddMissingMetaTags(doc);
        converter.Build();
        doc.Prettify().Trim().ShouldStartWith(result);
    }

}