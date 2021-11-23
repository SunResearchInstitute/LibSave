using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace LibSave.Types
{
    public abstract class SaveFile<T> : ISaveFile where T : new()
    {
        public T Data { get; protected set; }

        protected SaveFile(FileInfo filePath, T defaultData, bool isConfig = false) : base(filePath, isConfig)
        {
            LoadData(defaultData);
        }

        public override void Write() => FilePath.WriteAllText(JsonConvert.SerializeObject(Data, Formatting.Indented));

        public override async Task WriteAsync() => await Task.Run(() => Write());

        public override void Reload() => Reload(new T());

        public void Reload(T fallbackData) => LoadData(fallbackData);

        private void LoadData(T fallbackData)
        {
            if (FilePath.Exists)
            {
                try
                {
                    Data = JsonConvert.DeserializeObject<T>(FilePath.ReadAllText());
                    return;
                }
                catch { }
            }
            if (fallbackData == null)
                Data = new T();
            else
                Data = fallbackData;
        }
    }
}
