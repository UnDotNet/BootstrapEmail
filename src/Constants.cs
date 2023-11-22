
using System.Reflection;

namespace UnDotNet.BootstrapEmail;

public class Constants
{
    public static string? Version => Assembly.GetAssembly(typeof(Constants))?.GetName().Version?.ToString(3);
    public static string SassCacheHead { get; set; } = "";
    public static string SassCacheEmail { get; set; } = "";
}