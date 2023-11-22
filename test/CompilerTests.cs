
// ReSharper disable StringLiteralTypo
namespace UnDotNet.BootstrapEmail.Tests;

[TestClass]
public class CompilerTests
{
  
  [TestMethod]
  public void CompilesEmptyBody()
  {
    var input = """
                <html>
                  <body class="bg-light">
                  </body>
                </html>
                """;
    var expected = """
                   <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
                   <html>
                     <head>
                       <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
                       <meta http-equiv="x-ua-compatible" content="ie=edge">
                       <meta name="x-apple-disable-message-reformatting">
                       <meta name="viewport" content="width=device-width, initial-scale=1">
                       <meta name="format-detection" content="telephone=no, date=no, address=no, email=no">
                       
                       <!-- Compiled with Bootstrap Email DotNet version: 1.0.1 -->
                       <style type="text/css">body,table,td{font-family:Helvetica,Arial,sans-serif!important}.ExternalClass{width:100%}.ExternalClass,.ExternalClass p,.ExternalClass span,.ExternalClass font,.ExternalClass td,.ExternalClass div{line-height:150%}a{text-decoration:none}*{color:inherit}a[x-apple-data-detectors],u+#body a,#MessageViewBody a{color:inherit;text-decoration:none;font-size:inherit;font-family:inherit;font-weight:inherit;line-height:inherit}img{-ms-interpolation-mode:bicubic}table:not([class^=s-]){font-family:Helvetica,Arial,sans-serif;mso-table-lspace:0;mso-table-rspace:0;border-spacing:0;border-collapse:collapse}table:not([class^=s-]) td{border-spacing:0;border-collapse:collapse}@media screen and (max-width:600px){*[class*=s-lg-]>tbody>tr>td{font-size:0!important;line-height:0!important;height:0!important}}</style>
                     </head>
                     <body class="bg-light" style="margin: 0;padding: 0;border: 0;outline: 0;width: 100%;min-width: 100%;height: 100%;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;font-family: Helvetica, Arial, sans-serif;line-height: 24px;font-weight: normal;font-size: 16px;box-sizing: border-box;color: #000000;background-color: #f7fafc">
                       <table class="bg-light body" valign="top" role="presentation" border="0" cellpadding="0" cellspacing="0" style="margin: 0;padding: 0;border: 0;outline: 0;width: 100%;min-width: 100%;height: 100%;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;font-family: Helvetica, Arial, sans-serif;line-height: 24px;font-weight: normal;font-size: 16px;box-sizing: border-box;color: #000000;background-color: #f7fafc">
                         <tbody>
                           <tr>
                             <td valign="top" style="line-height: 24px;font-size: 16px;margin: 0;background-color: #f7fafc" align="left">
                             </td>
                           </tr>
                         </tbody>
                       </table>
                     </body>
                   </html>
                   
                   """;
    var compiler = new BootstrapCompiler(input);
    var output = compiler.Html();
        
    output.ShouldNotBeEmpty();
    output.ShouldNotBe(input);
    output.ShouldBe(expected);
  }
  
  [TestMethod]
  public void InlinesCss()
  {
    var input = """
                <html>
                  <body class="bg-light">
                <table>
                  <tbody>
                    <tr>
                      <td valign="top">
                      </td>
                    </tr>
                  </tbody>
                </table>
                  
                  </body>
                </html>
                """;
    var expected = """
                   <!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
                   <html>
                     <head>
                       <meta http-equiv="Content-Type" content="text/html; charset=utf-8">
                       <meta http-equiv="x-ua-compatible" content="ie=edge">
                       <meta name="x-apple-disable-message-reformatting">
                       <meta name="viewport" content="width=device-width, initial-scale=1">
                       <meta name="format-detection" content="telephone=no, date=no, address=no, email=no">
                       
                       <!-- Compiled with Bootstrap Email DotNet version: 1.0.1 -->
                       <style type="text/css">body,table,td{font-family:Helvetica,Arial,sans-serif!important}.ExternalClass{width:100%}.ExternalClass,.ExternalClass p,.ExternalClass span,.ExternalClass font,.ExternalClass td,.ExternalClass div{line-height:150%}a{text-decoration:none}*{color:inherit}a[x-apple-data-detectors],u+#body a,#MessageViewBody a{color:inherit;text-decoration:none;font-size:inherit;font-family:inherit;font-weight:inherit;line-height:inherit}img{-ms-interpolation-mode:bicubic}table:not([class^=s-]){font-family:Helvetica,Arial,sans-serif;mso-table-lspace:0;mso-table-rspace:0;border-spacing:0;border-collapse:collapse}table:not([class^=s-]) td{border-spacing:0;border-collapse:collapse}@media screen and (max-width:600px){*[class*=s-lg-]>tbody>tr>td{font-size:0!important;line-height:0!important;height:0!important}}</style>
                     </head>
                     <body class="bg-light" style="margin: 0;padding: 0;border: 0;outline: 0;width: 100%;min-width: 100%;height: 100%;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;font-family: Helvetica, Arial, sans-serif;line-height: 24px;font-weight: normal;font-size: 16px;box-sizing: border-box;color: #000000;background-color: #f7fafc">
                       <table class="bg-light body" valign="top" role="presentation" border="0" cellpadding="0" cellspacing="0" style="margin: 0;padding: 0;border: 0;outline: 0;width: 100%;min-width: 100%;height: 100%;-webkit-text-size-adjust: 100%;-ms-text-size-adjust: 100%;font-family: Helvetica, Arial, sans-serif;line-height: 24px;font-weight: normal;font-size: 16px;box-sizing: border-box;color: #000000;background-color: #f7fafc">
                         <tbody>
                           <tr>
                             <td valign="top" style="line-height: 24px;font-size: 16px;margin: 0;background-color: #f7fafc" align="left">
                               <table border="0" cellpadding="0" cellspacing="0">
                                 <tbody>
                                   <tr>
                                     <td valign="top" style="line-height: 24px;font-size: 16px;margin: 0" align="left">
                                     </td>
                                   </tr>
                                 </tbody>
                               </table>
                             </td>
                           </tr>
                         </tbody>
                       </table>
                     </body>
                   </html>
                   
                   """;
    var compiler = new BootstrapCompiler(input);
    compiler.CompileHtml();
    compiler.BuildPremailerDoc();
    compiler.ConfigureHtml();
    var output = compiler.FinalizeDocument();
    output.ShouldBe(expected);
  }

  [TestMethod]
  public void ShouldPurgeAllPaddingUtilsButTwo()
  {
    var input = """
                <p>Test</p>
                <table class="p-4">
                  <tbody>
                    <tr>
                      <td>Give me padding!</td>
                    </tr>
                  </tbody>
                </table>
                <!-- The trick here is the .btn is included in padding selectors for specific btn padding -->
                <!-- The .btn class was a culprit for not purging all padding selectors originally since they all contained the .htn in the selector -->
                <a class="btn p-5" href="#">Some button</a>
                """;
      
    var compiler = new BootstrapCompiler(input);
    var output = compiler.Html();

    var outputDoc = new HtmlParser().ParseDocument(output);
    outputDoc.Head?.InnerHtml.ShouldContain("p-4");
    outputDoc.Head?.InnerHtml.ShouldContain("p-5");
    outputDoc.Head?.InnerHtml.ShouldNotContain("p-6");
  }

  [TestMethod]
  public void ShouldCreateTableWithAlignProperty()
  {
    var input = """
                <div class="wrapper-1">
                   <a class="btn ax-center" href="#">Cool</a>
                 </div>
                """;
      
    var compiler = new BootstrapCompiler(input);
    var output = compiler.Html();

    var outputDoc = new HtmlParser().ParseDocument(output);
    var wrapper1 = outputDoc.QuerySelector(".wrapper-1 .ax-center");
    wrapper1.ShouldNotBeNull();
    wrapper1.Attributes["align"]?.Value.ShouldBe("center");

  }
    
}