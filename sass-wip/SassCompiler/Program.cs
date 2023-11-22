// Results:
/*
   | Method |       Mean |    Error |   StdDev |
   |------- |-----------:|---------:|---------:|
   |   V8Js |   295.8 ms |  4.39 ms |  4.11 ms |
   |   Jint | 7,615.3 ms | 71.52 ms | 63.40 ms |
 */

using System.Reflection;
using System.Security.Cryptography;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using DartSassHost;
using DartSassHost.Helpers;
using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.Jint;
using JavaScriptEngineSwitcher.V8;
using Microsoft.Extensions.FileProviders;

IJsEngineSwitcher engineSwitcher = JsEngineSwitcher.Current;
engineSwitcher.EngineFactories.AddJint();
engineSwitcher.EngineFactories.AddV8();
engineSwitcher.DefaultEngineName = V8JsEngine.EngineName;

const string inputContent = @"$font-stack: Helvetica, sans-serif;
$primary-color: #333;

body {
  font: 100% $font-stack;
  color: $primary-color;
}";

// var tests = new JintVsV8();
// Console.Write(tests.V8Js());

var options = new CompilationOptions
{
  SourceMap = false,
  IncludePaths = new List<string>()
  {
    "/Users/john/testdata/sass/"
  }
};

var _provider = new ManifestEmbeddedFileProvider(Assembly.GetAssembly(typeof(JintVsV8)));
var file = _provider.GetFileInfo("/bootstrap-email.scss");
using var reader = new StreamReader(file.CreateReadStream());
var contents = reader.ReadToEnd();


// var summary = BenchmarkRunner.Run<JintVsV8>();
try
{
  
  using var sassCompiler = new DartSassHost.SassCompiler(new V8JsEngineFactory(), new EmbeddedFileManager(), options);
  
  var startFullScss = """
                    @import 'scss/helpers';
                    @import 'scss/variables';
                    @import 'scss/reboot_email';
                    
                    @import 'scss/components/alert';
                    @import 'scss/components/badge';
                    @import 'scss/components/button';
                    @import 'scss/components/card';
                    @import 'scss/components/container';
                    @import 'scss/components/grid';
                    @import 'scss/components/hr';
                    @import 'scss/components/image';
                    @import 'scss/components/preview';
                    @import 'scss/components/table';
                    @import 'scss/components/stack';
                    
                    @import 'scss/utilities/background';
                    @import 'scss/utilities/border';
                    @import 'scss/utilities/border-radius';
                    @import 'scss/utilities/color';
                    @import 'scss/utilities/display';
                    @import 'scss/utilities/sizing';
                    @import 'scss/utilities/spacing';
                    @import 'scss/utilities/text-decoration';
                    @import 'scss/utilities/typography';
                    @import 'scss/utilities/valign';
                    """;
  var startScss = """
                  @import 'sass/scss/helpers';
                  """;
  
  CompilationResult result = sassCompiler.Compile(startFullScss, "input.scss",
    "output.css", "output.css.map", options);

  Console.WriteLine("Compiled content:{1}{1}{0}{1}", result.CompiledContent,
    Environment.NewLine);
  Console.WriteLine("Source map:{1}{1}{0}{1}", result.SourceMap, Environment.NewLine);
  Console.WriteLine("Included file paths: {0}",
    string.Join(", ", result.IncludedFilePaths));
}
catch (SassCompilerLoadException e)
{
  Console.WriteLine("During loading of Sass compiler an error occurred. See details:");
  Console.WriteLine();
  Console.WriteLine(SassErrorHelpers.GenerateErrorDetails(e));
}
catch (SassCompilationException e)
{
  Console.WriteLine("During compilation of SCSS code an error occurred. See details:");
  Console.WriteLine();
  Console.WriteLine(SassErrorHelpers.GenerateErrorDetails(e));
}
catch (SassException e)
{
  Console.WriteLine("During working of Sass compiler an unknown error occurred. See details:");
  Console.WriteLine();
  Console.WriteLine(SassErrorHelpers.GenerateErrorDetails(e));
}

Console.ReadLine();


public class JintVsV8
{
  private readonly string input = """
                                  $font-stack: Helvetica, sans-serif;
                                  $primary-color: #333;
                                  
                                  body {
                                    font: 100% $font-stack;
                                    color: $primary-color;
                                  }
                                  """;

  private CompilationOptions options = new() { SourceMap = true };

  [Benchmark]
  public string V8Js()
  {
    using var sassCompiler = new DartSassHost.SassCompiler(new V8JsEngineFactory(), options);
    CompilationResult result = sassCompiler.CompileFile("/Users/john/testdata/sass/bootstrap-email.scss",
      "output.css", "output.css.map", options);
    return result.CompiledContent;
  }

  [Benchmark]
  public string Jint()
  {
    using var sassCompiler = new DartSassHost.SassCompiler(new JintJsEngineFactory(), options);
    CompilationResult result = sassCompiler.CompileFile("/Users/john/testdata/sass/bootstrap-email.scss",
      "output.css", "output.css.map", options);
    return result.CompiledContent;
  }

}