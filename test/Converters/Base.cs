
namespace UnDotNet.BootstrapEmail.Tests.Converters;

[TestClass]
public class BaseTests
{
    [TestMethod]
    public void AddClass_RemovesAllExtraSpacesRegardlessOfInput()
    {
        var html = @"
            <div class="" some class    lots-of-spaces""></div>
        ";
        var parser = new HtmlParser();
        var doc = parser.ParseDocument(html);
        var converter = new Alert(doc);
        var el = doc.QuerySelector("div");
        el.ShouldNotBeNull();
        converter.AddClass(el, "   other   things lots of   more spaces");
        el.ClassName?.ShouldMatch("some class lots-of-spaces other things lots of more spaces");
    }
}