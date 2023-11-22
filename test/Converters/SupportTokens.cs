
namespace UnDotNet.BootstrapEmail.Tests.Converters;

[TestClass]
public class SupportUrlTokens
{
    [TestMethod]
    public void ShouldAllowBracketsInSrcAndHrefs()
    {
        var input = """
                   <html>
                     <head></head>
                     <body>
                       <img src="{{ some_code_here }}">
                       <a href="{{ some_code_here }}">Link</a>
                     </body>
                   </html>
                   """;
        var parser = new HtmlParser();
        var doc = parser.ParseDocument(input);
        var html = doc.Prettify();
        html = UnDotNet.BootstrapEmail.Converters.SupportUrlTokens.Replace(html);
        html.ShouldContain(@"<img src=""{{ some_code_here }}"">");
    }

    [TestMethod]
    public void ShouldAllowBracketsBeforeAfterAndBetweenElements()
    {
        var input = """
                    <html>
                    <head></head>
                    <body>
                      <img src="https://example.com/{{ some_code_here }}">
                      <a href="{{ some_code_here }}/example/com">Link</a>
                      <img src="https://example.com/{{ some_code_here }}/example/com">
                      <img src="https://{{ some_code_here }}.com/{{ some_code_here }}/example/com">
                    </body>
                    </html>
                    """;
        var parser = new HtmlParser();
        var doc = parser.ParseDocument(input);
        var html = doc.Prettify();
        html = UnDotNet.BootstrapEmail.Converters.SupportUrlTokens.Replace(html);
        html.ShouldContain(@"<img src=""https://example.com/{{ some_code_here }}"">");
        html.ShouldContain(@"<a href=""{{ some_code_here }}/example/com"">Link</a>");
        html.ShouldContain(@"<img src=""https://example.com/{{ some_code_here }}/example/com"">");
        html.ShouldContain(@"<img src=""https://{{ some_code_here }}.com/{{ some_code_here }}/example/com"">");
    }

}
