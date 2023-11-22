using System.Web;

namespace UnDotNet.BootstrapEmail.Converters;

internal class SupportUrlTokens(IHtmlDocument doc) : Base<SupportUrlTokens>(doc)
{
    private static readonly string OpenBrackets = HttpUtility.UrlEncode("{{");
    private static readonly string OpenPercent = HttpUtility.UrlEncode("{") + "%";
    private static readonly string CloseBrackets = HttpUtility.UrlEncode("}}");
    private static readonly string ClosePercent = "%" + HttpUtility.UrlEncode("}");

    public static string Replace(string html)
    {
        // {Regex.Escape(OPEN_BRACKETS)}
        // {Regex.Escape(OPEN_PERCENT)}
        // {Regex.Escape(CLOSE_BRACKETS)}
        // {Regex.Escape(CLOSE_PERCENT)}
        // Regex regex = new Regex(@"((href|src)=(""|'))(.*?((OPEN_BRACKETS|OPEN_PERCENT).*?(CLOSE_BRACKETS|CLOSE_PERCENT)).*?)(""|')");
        var regex = new Regex(@$"((href|src)=(""|'))(.*?(({Regex.Escape(OpenBrackets)}|{Regex.Escape(OpenPercent)}).*?({Regex.Escape(CloseBrackets)}|{Regex.Escape(ClosePercent)})).*?)(""|')");
        if (!regex.IsMatch(html))
        {
            return html;
        }
        // Regex innerRegex = new Regex(@"((OPEN_BRACKETS|OPEN_PERCENT).*?(CLOSE_BRACKETS|CLOSE_PERCENT))");
        var innerRegex = new Regex(@$"(({Regex.Escape(OpenBrackets)}|{Regex.Escape(OpenPercent)}).*?({Regex.Escape(CloseBrackets)}|{Regex.Escape(ClosePercent)}))");
        html = regex.Replace(html, match =>
        {
            var startText = match.Groups[1].Value;
            var middleText = match.Groups[4].Value;
            var endText = match.Groups[8].Value;
            middleText = innerRegex.Replace(middleText, m => HttpUtility.UrlDecode(m.Value));
            return startText + middleText + endText;
        });
        return html;
    }
}