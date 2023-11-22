using System.Reflection;
using DartSassHost;
using Microsoft.Extensions.FileProviders;

public class EmbeddedFileManager : IFileManager 
{
    public bool SupportsVirtualPaths => false;
    public IFileProvider _provider = new ManifestEmbeddedFileProvider(Assembly.GetAssembly(typeof(JintVsV8)));

    public string GetCurrentDirectory() => "/"; //currentDir;

    public bool FileExists(string path) =>_provider.GetFileInfo(path).Exists;
    
    public string ReadFile(string path)
    {
        var info = _provider.GetFileInfo(path);
        if (!info.Exists) throw new NullReferenceException($"{path} does not exist");
        using var reader = new StreamReader(info.CreateReadStream());
        var contents = reader.ReadToEnd();
        // currentDir = Path.GetDirectoryName(path)!;
        return contents;
    }

    public bool IsAppRelativeVirtualPath(string path) => throw new NotImplementedException();
    public string ToAbsoluteVirtualPath(string path) => throw new NotImplementedException();
    
}
