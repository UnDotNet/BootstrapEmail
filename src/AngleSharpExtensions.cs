using AngleSharp;

namespace UnDotNet.BootstrapEmail;

public static class AngleSharpExtensions
{
    public static String Prettify(this IMarkupFormattable markup) =>
        markup.ToHtml(new PrettyMarkupFormatter { Indentation = "  " }).Trim();

    // ReSharper disable once UnusedMember.Global
    public static String ToEmailHtml(this IMarkupFormattable markup) =>
        markup.ToHtml(new HtmlMarkupFormatter()).Trim();
}
