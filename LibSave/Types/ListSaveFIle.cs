using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Threading;

namespace LibSave.Types
{
    //Follow normal list specs
    public class ListSaveFile<T> : SaveFile<List<T>>, IList<T>, IReadOnlyList<T>, IList
    {
        [NonSerialized]
        private readonly Action<ulong, List<T>> _cleanupAction;
        [NonSerialized]
        private object _syncRoot;

        //Should only be used if list is going to be UlongList
        public ListSaveFile(string name) : base(name)
        {
            if (typeof(T) != typeof(ulong))
                throw new Exception("Default constructor should only be used for ulong and being used for discord guilds.");

            _cleanupAction = delegate (ulong guild, List<T> items) { (items as List<ulong>).Remove(guild); };
        }
        public ListSaveFile(string name, Action<ulong, List<T>> cleanUp) : base(name) => _cleanupAction = cleanUp;

        public override void CleanUp(ulong id) => _cleanupAction?.Invoke(id, _data);

        public void Set(List<T> newList) => _data = newList;

        public T this[int index] { get => _data[index]; set => _data[index] = value; }
        object IList.this[int index] { get => _data[index]; set => _data[index] = (T)value; }

        public int Count => _data.Count;

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

        public void Add(T item) => _data.Add(item);

        public int Add(object value)
        {
            if (value == null && !(default(T) == null))
                throw new ArgumentNullException();

            Add((T)value);

            return Count - 1;
        }

        public void Clear() => _data.Clear();

        public bool Contains(T item) => _data.Contains(item);

        public bool Contains(object value) => _data.Contains((T)value);

        public void CopyTo(T[] array, int arrayIndex) => _data.CopyTo(array, arrayIndex);

        public void CopyTo(Array array, int index)
        {
            if ((array != null) && (array.Rank != 1))
                throw new ArgumentException();

            Contract.EndContractBlock();

            // Array.Copy will check for NULL.
            Array.Copy(_data.ToArray(), 0, array, index, _data.Count);
        }

        public IEnumerator<T> GetEnumerator() => _data.GetEnumerator();

        public int IndexOf(T item) => _data.IndexOf(item);

        public int IndexOf(object value) => _data.IndexOf((T)value);

        public void Insert(int index, T item) => _data.Insert(index, item);

        public void Insert(int index, object value) => _data.Insert(index, (T)value);

        public bool Remove(T item) => _data.Remove(item);

        public void Remove(object value) => _data.Remove((T)value);

        public void RemoveAt(int index) => _data.RemoveAt(index);

        IEnumerator IEnumerable.GetEnumerator() => _data.GetEnumerator();
    }
}
