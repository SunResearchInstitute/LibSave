using System.IO;

namespace LibSave
{
    internal static class Utils
    {
        internal static FileInfo GetFile(this DirectoryInfo obj, string filename) => new FileInfo($"{obj.FullName}{Path.DirectorySeparatorChar}{filename}");
        internal static string ReadAllText(this FileInfo obj) => File.ReadAllText(obj.FullName);

        internal static void WriteAllText(this FileInfo obj, string contents) => File.WriteAllText(obj.FullName, contents);
    }
}
