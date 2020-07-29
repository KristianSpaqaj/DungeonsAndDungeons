using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DungeonsAndDungeons
{
    /// <summary>
    /// Represents a collection of Items
    /// </summary>
    public class Inventory : IList
    {
        private List<Item> Items { get; }

        public Inventory()
        {
            Items = new List<Item>();
        }

        public Inventory(params Item[] items)
        {
            Items = items.ToList();
        }

        public object this[int index] { get => Items[index]; set => Items[index] = (Item)value; }

        public bool IsReadOnly => false;

        public bool IsFixedSize => true;

        public int Count => Items.Count;

        public object SyncRoot => null;

        public bool IsSynchronized => false;

        public int Add(object value)
        {
            Items.Add((Item)value);
            return Count;
        }

        public void Clear()
        {
            Items.Clear();
        }

        public bool Contains(object value)
        {
            return Items.Contains((Item)value);
        }

        public void CopyTo(Array array, int index)
        {
            for (int i = index; i < array.Length; i++)
            {
                array.SetValue(Items[i], i);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return Items.GetEnumerator();
        }

        public int IndexOf(object value)
        {
            return Items.IndexOf((Item)value);
        }

        public void Insert(int index, object value)
        {
            Items.Insert(index, (Item)value);
        }

        public void Remove(object value)
        {
            Items.Remove((Item)value);
        }

        public void RemoveAt(int index)
        {
            Items.RemoveAt(index);
        }
    }
}
