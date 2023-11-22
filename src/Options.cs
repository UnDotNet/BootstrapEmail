/*
 Future use
namespace UnDotNet.BootstrapEmail;

public class Options
{
    /// <summary>
    /// Any SCSS included here will be prepended to the head SCSS prior to compilation
    /// The resulting head CSS is not inlined to the email body
    /// Only used if the Sass plugin is available
    /// </summary>
    public string PrependHeadScss { get; set; }
    
    /// <summary>
    /// SCSS provided here will replace the entire starting head SCSS entry.
    /// If it's a filename, it will be used as a starting point.  If not,
    /// it will be used as the contents of a starting file.
    /// The resulting head CSS is not inlined to the email body
    /// Only used if the Sass plugin is available
    /// </summary>
    public string ReplaceHeadScss { get; set; }
    
    /// <summary>
    /// Any CSS here will be appended to the default CSS embedded in the head.
    /// The head CSS is not inlined to the email body
    /// This allows overriding without requiring the full SASS plugin.
    /// </summary>
    public string AppendHeadCss { get; set; }

    /// <summary>
    /// Any SCSS included here will be prepended to the email SCSS prior to compilation
    /// The resulting email CSS is inlined to the email body
    /// Only used if the Sass plugin is available
    /// </summary>
    public string PrependEmailScss { get; set; }
    
    /// <summary>
    /// SCSS provided here will replace the entire starting email SCSS entry.
    /// If it's a filename, it will be used as a starting point.  If not,
    /// it will be used as the contents of a starting file.
    /// The resulting email CSS is inlined to the email body
    /// Only used if the Sass plugin is available
    /// </summary>
    public string ReplaceEmailScss { get; set; }
    
    /// <summary>
    /// Any CSS here will be appended to the default email CSS.
    /// The email CSS is inlined to the email body
    /// This allows overriding without requiring the full SASS plugin.
    /// </summary>
    public string AppendEmailCss { get; set; }
}
*/