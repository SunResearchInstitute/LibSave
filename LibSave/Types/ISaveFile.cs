using System.IO;
using System.Threading.Tasks;

namespace LibSave.Types
{
    public abstract class ISaveFile
    {
        public FileInfo FilePath;

        protected ISaveFile(FileInfo filePath)
        {
            FilePath = filePath;
        }

        public abstract void Write();

        public abstract Task WriteAsync();

        public abstract void Reload();
    }
}
