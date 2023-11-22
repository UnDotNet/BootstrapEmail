
namespace UnDotNet.BootstrapEmail.Tests.Converters;

[TestClass]
public class BadgeTests
{
  [TestMethod]
  public void CanGenerateBadge()
  {
    var html = """<span class="badge badge-primary">Badge</span>""";
    var result = """
                 <table class="badge badge-primary" align="left" role="presentation">
                   <tbody>
                     <tr>
                       <td>
                         <span>Badge</span>
                       </td>
                     </tr>
                   </tbody>
                 </table>
                 """;
        
    var parser = new HtmlParser();
    var doc = parser.ParseDocument(html);
    new Badge(doc).Build();
    doc.Body?.InnerHtml.ShouldMatch(result);
  }
   
}