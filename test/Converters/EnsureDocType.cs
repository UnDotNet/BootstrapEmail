
namespace UnDotNet.BootstrapEmail.Tests.Converters;

[TestClass]
public class EnsureDocTypeTests
{
    [TestMethod]
    public void ShouldAddDocTypeWhenMissing()
    {
        var input = """
                    <html>
                    <head>
                    </head>
                    </html>
                    """;

        var compiler = new BootstrapCompiler(input);
        compiler.CompileHtml();
        compiler.BuildPremailerDoc();
        compiler.ConfigureHtml();
        var html = compiler.FinalizeDocument();
        html.ShouldContain(@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">");
    }

    [TestMethod]
    public void ShouldAddDocTypeWhenMissingWithHeadContent()
    {
        var input = """
                    <html>
                    <head>
                    <meta></meta>
                    </head>
                    </html>
                    """;

        var compiler = new BootstrapCompiler(input);
        compiler.CompileHtml();
        compiler.BuildPremailerDoc();
        compiler.ConfigureHtml();
        var html = compiler.FinalizeDocument();
        html.ShouldContain(@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">");
    }

    [TestMethod]
    public void ShouldReplaceWrongDocTypeWithCorrectOne()
    {
        var input = """
                    <!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" "http://www.w3.org/TR/REC-html40/loose.dtd">
                    <html>
                     <head></head>
                     <body>
                     </body>
                    </html>
                    """;

        var compiler = new BootstrapCompiler(input);
        compiler.CompileHtml();
        compiler.BuildPremailerDoc();
        compiler.ConfigureHtml();
        var html = compiler.FinalizeDocument();
        html.ShouldContain(@"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">");
    }


}