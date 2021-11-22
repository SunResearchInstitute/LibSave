using LibSave.Types;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace LibSave
{
    public class SaveController
    {
        public DirectoryInfo SavePath { get; private set; }
        public Dictionary<string, ISaveFile> SaveFiles { get; private set; }

        public SaveController(string directoryPath)
        {
            SavePath = new DirectoryInfo(directoryPath);
            if (!SavePath.Exists)
                SavePath.Create();
            SaveFiles = new Dictionary<string, ISaveFile>();
        }

        public void RegisterDictionarySave<K, V>(string saveName, Dictionary<K, V> defaultData = null)
        {
            if (!SaveFiles.ContainsKey(saveName))
            {
                DictionarySaveFile<K, V> save = new DictionarySaveFile<K, V>(GetSavePath(saveName), defaultData);
                SaveFiles.Add(saveName, save);
            }
        }

        public void RegisterListSave<T>(string saveName, List<T> defaultData = null)
        {
            if (!SaveFiles.ContainsKey(saveName))
            {
                ListSaveFile<T> save = new ListSaveFile<T>(GetSavePath(saveName), defaultData);
                SaveFiles.Add(saveName, save);
            }
        }

        public void ClearSaveFiles()
        {
            SaveFiles.Clear();
        }

        public void SaveAll(bool clean = false)
        {
            foreach (KeyValuePair<string, ISaveFile> save in SaveFiles)
            {
                if (!save.Value.isConfig)
                    save.Value.Write();
            }

            if (clean)
                ClearSaveFiles();
        }

        public async Task SaveAllAsync(bool clean = false) => await Task.Run(() => SaveAll(clean));

        public void ReloadAll()
        {
            foreach (KeyValuePair<string, ISaveFile> save in SaveFiles)
                save.Value.Reload();
        }

        public void RegisterCustomSave<T>(string saveName, T save) where T : ISaveFile => SaveFiles.Add(saveName, save);

        public T GetSave<T>(string saveName) where T : ISaveFile
        {
            if (SaveFiles.ContainsKey(saveName))
            {
                return (T)SaveFiles[saveName];
            }
            return null;
        }

        public bool RemoveSave(string saveName) => SaveFiles.Remove(saveName);

        public FileInfo GetSavePath(string saveName) => SavePath.GetFile(saveName);
    }
}
