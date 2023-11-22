![bootstrapemail](https://raw.githubusercontent.com/UnDotNet/BootstrapEmail/main/assets/bootstrapemail_logo.png) 
# UnDotNet.BootstrapEmail

Ported from [BootstrapEmail](https://bootstrapemail.com).  Their excellent [templates](https://app.bootstrapemail.com/templates/pricing) compile fine under .NET.  Highly recommend checking them out!

If you know Bootstrap, you know Bootstrap Email.

[Explore Bootstrap Email docs](https://v1.bootstrapemail.com/docs/introduction)

Bootstrap Email takes most of its inspiration from these two wonderful frameworks, [Bootstrap](https://getbootstrap.com) and [Tailwind](https://tailwindcss.com) but for HTML emails. Working with HTML in emails is never easy because of the nuances of email vs the web. With Bootstrap Email you don't have to understand all the nuance and it allows you to write emails like you would a website.

## Setup
There are a few different ways you can use Bootstrap Email to compile emails:

- [NuGet Package](https://bootstrapemail.com/docs/usage#command-line)
- [Online Editor](https://app.bootstrapemail.com/editor)
- [Original (Non-DotNet) Versions](https://github.com/bootstrap-email/bootstrap-email)

### Installation

Add NuGet package `UnDotNet.BootstrapEmail` to your project

### Basic Usage

```csharp
using UnDotNet.BootstrapEmail;

var compiler = new BootstrapCompiler(source);

// both htlm & plaintext
var result = compiler.Multipart();

// only text
var text = compiler.Text();

// only html
var text = compiler.Html();
```

### Not Implemented Features
- SASS compilation: currently, the SASS is compiled to CSS in the project and not re-compiled on every build.  My goal is to make this fast enough to run as needed rather than saving the compiled html somewhere.
- Options: The options from [BootstrapEmail.com](https://bootstrapemail.com/docs/configure) are all related to SASS compilation so they are not currently implemented either.

### Documentation
For full documentation, visit [bootstrapemail.com](https://bootstrapemail.com/docs/introduction)
