using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading;

namespace LibSave.Types
{
    //Follow normal dictionary specs
    public class DictionarySaveFile<T, TK> : SaveFile<Dictionary<T, TK>>, IDictionary<T, TK> where T : notnull
    {
        [NonSerialized]
        private object _syncRoot;

        public DictionarySaveFile(FileInfo file, Dictionary<T, TK> defaultData = null) : base(file, defaultData) { }

        public void Set(Dictionary<T, TK> newDictionary) => Data = newDictionary;

        public TK this[T key] { get => Data[key]; set => Data[key] = value; }
        public object this[object key] { get => Data[(T)key]; set => Data[(T)key] = (TK)value; }

        public ICollection<T> Keys => Data.Keys;

        public ICollection<TK> Values => Data.Values;

        public int Count => Data.Count;

        public bool IsReadOnly => false;

        public bool IsFixedSize => false;

        public bool IsSynchronized => false;

        public object SyncRoot
        {
            get
            {
                if (_syncRoot == null)
                    Interlocked.CompareExchange<object>(ref _syncRoot, new object(), null);

                return _syncRoot;
            }
        }
        public void Add(T key, TK value) => Data.Add(key, value);

        public void Add(KeyValuePair<T, TK> item) => Data.Add(item.Key, item.Value);

        public void Add(object key, object value) => Data.Add((T)key, (TK)value);

        public void Clear() => Data.Clear();

        public bool Contains(KeyValuePair<T, TK> item) => Data.ContainsKey(item.Key) && Data.ContainsValue(item.Value);

        public bool Contains(object key) => Data.ContainsKey((T)key);

        public bool ContainsKey(T key) => Data.ContainsKey(key);

        public void CopyTo(KeyValuePair<T, TK>[] array, int arrayIndex) => Data.ToArray().CopyTo(array, arrayIndex);

        public IEnumerator<KeyValuePair<T, TK>> GetEnumerator() => Data.GetEnumerator();

        public void GetObjectData(SerializationInfo info, StreamingContext context) => Data.GetObjectData(info, context);

        public void OnDeserialization(object sender) => Data.OnDeserialization(sender);

        public bool Remove(T key) => Data.Remove(key);

        // ReSharper disable once UseDeconstructionOnParameter
        public bool Remove(KeyValuePair<T, TK> item)
        {
            if (item.Equals(null))
                return false;

            if (Data.ContainsKey(item.Key) && Data[item.Key].Equals(item.Value))
                return Data.Remove(item.Key);

            return false;
        }

        public void Remove(object key) => Data.Remove((T)key);

        public bool TryGetValue(T key, [MaybeNullWhen(false)] out TK value) => Data.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => Data.GetEnumerator();
    }
}
