namespace UnDotNet.BootstrapEmail.Converters;

internal class EnsureDocType(IHtmlDocument doc) : Base<EnsureDocType>(doc)
{
    private static readonly string CorrectDocType =
        @"<!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Strict//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd"">";

    public override void Build() { }

    public static string Replace(string html)
    {
//         return html;
        //     ensure the proper XHTML doctype which ensures best compatibility in email clients
        //   https://github.com/bootstrap-email/bootstrap-email/discussions/168
        var exists = Regex.IsMatch(html, @"^<!DOCTYPE.*(\[[\s\S]*?\])?>");
        if (!exists) return CorrectDocType + "\n" + html;
        return Regex.Replace(html, @"<!DOCTYPE.*(\[[\s\S]*?\])?>",
            CorrectDocType);
    }
}
