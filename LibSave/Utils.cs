using System.IO;
using System.Text;

namespace LibSave
{
    internal static class Utils
    {
        internal static FileInfo GetFile(this DirectoryInfo obj, string filename) => new FileInfo($"{obj.FullName}{Path.DirectorySeparatorChar}{filename}");
        internal static string ReadAllText(this FileInfo obj) => File.ReadAllText(obj.FullName);
        internal static string ReadAllText(this FileInfo obj, Encoding encoding) => File.ReadAllText(obj.FullName, encoding);

        internal static void WriteAllText(this FileInfo obj, string contents) => File.WriteAllText(obj.FullName, contents);
        internal static void WriteAllText(this FileInfo obj, string contents, Encoding encoding) => File.WriteAllText(obj.FullName, contents, encoding);
    }
}
