using System.Globalization;
using System.Reflection;
using dotenv.net;
using dotenv.net.Utilities;
using FluentEmail.Core;
using UnDotNet.BootstrapEmail;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);
var postmarkKey = EnvReader.GetStringValue("POSTMARK_TOKEN");
// Add services to the container.
builder.Services.AddFluentEmail("hi@undotnet.com").AddPostmarkSender(postmarkKey);
// Like with Microsoft.AspNetCore.SpaProxy, a 'spa.proxy.json' file gets generated based on the values in the project file (SpaRoot, SpaClientUrl, SpaLaunchCommand).
// This file gets not published when using "dotnet publish".
// The services get not added and the proxy is not used when the file does not exist.
builder.Services.AddSpaYarp();

builder.Services.AddCors(o => o.AddPolicy("Any", p =>
{
    p.AllowAnyOrigin();
    p.AllowAnyMethod();
    p.AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseCors("Any");
}

app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapPost("/render", (HtmlInput model) =>
{
    var compiler = new BootstrapCompiler(model.Source);
    var result = compiler.Multipart();
    return result;
});
app.MapPost("/send", async (IFluentEmail email, SendModel model) =>
{
    await email.To(model.Email).Subject("UnDotNet.Bootstrap Test Email").UsingBootstrap(model.Source).SendAsync();
    return new MultipartResult(email.Data.PlaintextAlternativeBody, email.Data.Body);
});

// The middleware and route endpoint get only added if the 'spa.proxy.json' file exists and the SpaYarp services were added.
// uncomment this if you want to proxy the Nuxt dev server through the .NET Core hosting
// app.UseSpaYarp();

// If the SPA proxy is used, this will never be reached.
app.MapFallbackToFile("index.html");

app.Run();

record HtmlInput(string Source) { }
record SendModel(string Source, string Email) { }

public static class FluentEmailBootstrap
{
    public static IFluentEmail CompileBootstrap(this IFluentEmail email, bool updateText = true)
    {
        var compiler = new BootstrapCompiler(email.Data.Body);
        var result = compiler.Multipart();
        email.Data.Body = result.Html;
        email.Data.IsHtml = true;
        if (updateText)
        {
            email.PlaintextAlternativeBody(result.Text);
        }
        return email;
    }
    
    /// <summary>
    /// Adds Bootstrap body to the email
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="body">The body</param>
    /// <param name="isHtml">True if Body is HTML, false for plain text (Optional)</param>
    /// <returns>Instance of the Email class</returns>
    public static IFluentEmail UsingBootstrap(this IFluentEmail email, string body, bool isHtml = true)
    {
        email.Body(body, isHtml);
        email.CompileBootstrap(!isHtml);
        return email;
    }

    /// <summary>
    /// Adds Bootstrap template to the email
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="template">The template</param>
    /// <param name="model">Model for the template</param>
    /// <param name="isHtml">True if Body is HTML, false for plain text (Optional)</param>
    /// <returns>Instance of the Email class</returns>
    public static IFluentEmail UsingBootstrapTemplate<T>(this IFluentEmail email, string template, T model, bool isHtml = true)
    {
        email.UsingTemplate(template, model, isHtml);
        email.CompileBootstrap(!isHtml);
        return email;
    }
    
    /// <summary>
    /// Adds Bootstrap template to email from embedded resource
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">Path the the embedded resource eg [YourAssembly].[YourResourceFolder].[YourFilename.txt]</param>
    /// <param name="model">Model for the template</param>
    /// <param name="assembly">The assembly your resource is in. Defaults to calling assembly.</param>
    /// <param name="isHtml">True if Body is HTML (default), false for plain text</param>
    /// <returns></returns>
    public static IFluentEmail UsingBootstrapTemplateFromEmbedded<T>(this IFluentEmail email, string path, T model, Assembly assembly, bool isHtml = true)
    {
        email.UsingTemplateFromEmbedded(path, model, assembly, isHtml);
        email.CompileBootstrap(!isHtml);
        return email;
    }
    
    /// <summary>
    /// Adds Bootstrap template to email from previously configured default embedded resource
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="path">Path the the embedded resource eg [YourResourceFolder].[YourFilename.txt]. Will be appended to configured root path</param>
    /// <param name="model">Model for the template</param>
    /// <param name="isHtml">True if Body is HTML (default), false for plain text</param>
    /// <returns></returns>
    public static IFluentEmail UsingBootstrapTemplateFromEmbedded<T>(this IFluentEmail email, string path, T model, bool isHtml = true)
    {
        email.UsingTemplateFromEmbedded(path, model, isHtml);
        email.CompileBootstrap(!isHtml);
        return email;
    }

    /// <summary>
    /// Adds Bootstrap template to email from embedded resource
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filename">The path to the file to load</param>
    /// <param name="model">Model for the template</param>
    /// <param name="isHtml">True if Body is HTML (default), false for plain text</param>
    /// <returns></returns>
    public static IFluentEmail UsingBootstrapTemplateFromFile<T>(this IFluentEmail email, string filename, T model, bool isHtml = true)
    {
        email.UsingTemplateFromFile(filename, model, isHtml);
        email.CompileBootstrap(!isHtml);
        return email;
    }
    
    /// <summary>
    /// Adds Bootstrap template to email from previously configured default embedded resource
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filename">The path to the file to load</param>
    /// <param name="model">Model for the template</param>
    /// <param name="culture">The culture of the template (Default is the current culture)</param>
    /// <param name="isHtml">True if Body is HTML (default), false for plain text</param>
    /// <returns></returns>
    public static IFluentEmail UsingBootstrapCultureTemplateFromFile<T>(this IFluentEmail email, string filename, T model, CultureInfo culture, bool isHtml = true)
    {
        email.UsingCultureTemplateFromFile(filename, model, culture, isHtml);
        email.CompileBootstrap(!isHtml);
        return email;
    }

    
    
}