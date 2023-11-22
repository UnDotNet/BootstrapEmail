namespace UnDotNet.BootstrapEmail.Converters;

internal class BeautifyHtml(IHtmlDocument doc) : Base<BeautifyHtml>(doc)
{
    public static string Replace(string html)
    {
        // https://github.com/AngleSharp/AngleSharp/issues/869
        html = html.Replace("&nbsp;", "~NBS1~");
        html = html.Replace("&u160;", "~NBS2~");
        // html = Regex.Replace(html, @"&nbsp; (?=[ \r\n\t<\/])", "~NBS1~", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);
        // html = Regex.Replace(html, @"(?<=>)&#160; (?=[ \r\n\t<\/])", "~NBS2~", RegexOptions.IgnoreCase | RegexOptions.IgnorePatternWhitespace);

        var oDocument = new HtmlParser().ParseDocument(html);
        var sw = new StringWriter();
        oDocument.ToHtml(sw, new PrettyMarkupFormatter() { Indentation = "  " });
        var prettyHtml = sw.ToString();
        prettyHtml = prettyHtml.Replace("~NBS1~", "&#160;");
        prettyHtml = prettyHtml.Replace("~NBS2~", "&#160;");
        return prettyHtml;
    }
}
