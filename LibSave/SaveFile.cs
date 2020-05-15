using Newtonsoft.Json;

namespace LibSave
{
    public abstract class SaveFile<T> : ISaveFile where T : new()
    {
        protected T Data;

        protected SaveFile(string name)
        {
            FileInfo = SaveDirectory.GetFile(name);
            Data = FileInfo.Exists ? JsonConvert.DeserializeObject<T>(FileInfo.ReadAllText()) : new T();
        }

        public override void Write() => FileInfo.WriteAllText(JsonConvert.SerializeObject(Data, Formatting.Indented));
    }
}
