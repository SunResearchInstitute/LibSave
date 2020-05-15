using System.IO;

namespace LibSave
{
    // ReSharper disable once InconsistentNaming
    public abstract class ISaveFile
    {
        // ReSharper disable once MemberCanBeProtected.Global
        public static readonly DirectoryInfo SaveDirectory = new DirectoryInfo("Save");

        // ReSharper disable once MemberCanBeProtected.Global
        public FileInfo FileInfo;

        static ISaveFile()
        {
            if (!SaveDirectory.Exists)
                SaveDirectory.Create();
        }

        public abstract void Write();

        public abstract void CleanUp(ulong id);
    }
}
