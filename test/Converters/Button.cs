
namespace UnDotNet.BootstrapEmail.Tests.Converters;

[TestClass]
public class ButtonTests
{
  [TestMethod]
  public void CanGenerateButton()
  {
    var input = """
                <div class="wrapper-1">
                  <a class="btn ax-center" href="#">Cool</a>
                </div>
                """;
    var result = """
                 <div class="wrapper-1">
                   <table class="btn ax-center" role="presentation">
                   <tbody>
                     <tr>
                       <td>
                         <a href="#">Cool</a>
                       </td>
                     </tr>
                   </tbody>
                 </table>
                 </div>
                 """;
      
    var parser = new HtmlParser();
    var doc = parser.ParseDocument(input);
    new Button(doc).Build();
        
    doc.Body?.InnerHtml.ShouldBe(result);
  }

}