using System.IO;
using System.Threading.Tasks;

namespace LibSave.Types
{
    public abstract class ISaveFile
    {
        public FileInfo FilePath;

        public bool isConfig;

        protected ISaveFile(FileInfo filePath, bool isConfig)
        {
            FilePath = filePath;
            this.isConfig = isConfig;
        }

        public abstract void Write();

        public abstract Task WriteAsync();

        public abstract void Reload();
    }
}
