using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace LibSave.Types
{
    public abstract class SaveFile<T> : ISaveFile where T : new()
    {
        protected T Data;

        protected SaveFile(FileInfo filePath, T defaultData) : base(filePath)
        {
            LoadData(defaultData);
        }

        public override void Write() => FilePath.WriteAllText(JsonConvert.SerializeObject(Data, Formatting.Indented));

        public override Task WriteAsync() => Task.Run(() => Write());

        public override void Reload() => Reload(new T());

        public void Reload(T data) => LoadData(data);

        private void LoadData(T fallbackData)
        {
            if (FilePath.Exists)
            {
                try
                {
                    Data = JsonConvert.DeserializeObject<T>(FilePath.ReadAllText());
                }
                catch
                {
                    if (fallbackData == null)
                    {
                        Data = new T();
                    }
                    else
                    {
                        Data = fallbackData;
                    }
                }
            }
        }
    }
}
