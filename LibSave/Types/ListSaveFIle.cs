using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Threading;

namespace LibSave.Types
{
    //Follow normal list specs
    public class ListSaveFile<T> : SaveFile<List<T>>, IList<T>
    {
        [NonSerialized]
        private object _syncRoot;

        //Should only be used if list is going to be UlongList
        public ListSaveFile(FileInfo filePath, List<T> defaultData = null) : base(filePath, defaultData) { }

        public void Set(List<T> newList) => Data = newList;

        public T this[int index] { get => Data[index]; set => Data[index] = value; }

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

        public void Add(T item) => Data.Add(item);

        public int Add(object value)
        {
            if (value == null && default(T) != null)
                throw new ArgumentNullException();

            Add((T)value);

            return Count - 1;
        }

        public void Clear() => Data.Clear();

        public bool Contains(T item) => Data.Contains(item);

        public bool Contains(object value) => Data.Contains((T)value);

        public void CopyTo(T[] array, int arrayIndex) => Data.CopyTo(array, arrayIndex);

        public void CopyTo(Array array, int index)
        {
            if ((array != null) && (array.Rank != 1))
                throw new ArgumentException();

            Contract.EndContractBlock();

            // Array.Copy will check for NULL.
            Array.Copy(Data.ToArray(), 0, array, index, Data.Count);
        }

        public IEnumerator<T> GetEnumerator() => Data.GetEnumerator();

        public int IndexOf(T item) => Data.IndexOf(item);

        public int IndexOf(object value) => Data.IndexOf((T)value);

        public void Insert(int index, T item) => Data.Insert(index, item);

        public void Insert(int index, object value) => Data.Insert(index, (T)value);

        public bool Remove(T item) => Data.Remove(item);

        public void Remove(object value) => Data.Remove((T)value);

        public void RemoveAt(int index) => Data.RemoveAt(index);

        IEnumerator IEnumerable.GetEnumerator() => Data.GetEnumerator();
    }
}
