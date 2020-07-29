using System.Collections.Generic;
using System.Linq;

namespace DungeonsAndDungeons
{
    /// <summary>
    /// Represents a collection of Items
    /// </summary>
    public class Inventory
    {

        public List<Item> Items { get; set; }
        public int ItemLimit { get; set; }

        public Inventory()
        {
            Items = new List<Item>();
            ItemLimit = int.MaxValue;
        }

        public Inventory(List<Item> items, int itemLimit = int.MaxValue)
        {
            Items = items;
            ItemLimit = itemLimit;
        }

        public void AddItem(Item item)
        {
            Items.Add(item);
        }

        public Item GetItem(int index)
        {
            return Items.ElementAt(index);
        }

        public List<Item> GetAllItems()
        {
            return Items;
        }

        public void DropItem(int index)
        {
            Items.RemoveAt(index);
        }

    }
}
