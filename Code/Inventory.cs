using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace DungeonsAndDungeons
{
    /// <summary>
    /// Represents a collection of Items
    /// </summary>
    public class Inventory : IEnumerable<Item>
    {
        public int Size { get; }
        private Item[] Items { get; }

        public Inventory(int size)
        {
            Size = size;
            Items = new Item[size];
        }

        public Inventory(int size, IList<Item> items) : this(size)
        {
            for (int i = 0; i < items.Count; i++)
            {
                Items[i] = items[i];
            }
        }

        public Item this[int i] { get => Items[i]; set => Items[i] = value; }


        public bool Remove(Item item)
        {
            for (int i = Size - 1; i >= 0; i--)
            {
                if (Items[i] == item)
                {
                    Items[i] = null;
                    return true;
                }
            }

            return false;
        }

        public void Add(Item item)
        {
            for (int i = 0; i < Size; i++)
            {
                if (Items[i] != null)
                {
                    Items[i] = item;
                    return;
                }
            }

            throw new InvalidOperationException("Inventory is full");
        }

        public bool IsSlotEmpty(int i)
        {
            return Items[i] == null;
        }

        public IEnumerator<Item> GetEnumerator()
        {
            return Items.Take(Items.Length).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Items.GetEnumerator();
        }

    }
}
