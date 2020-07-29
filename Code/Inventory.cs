using System.Collections.Generic;
using System.Linq;

namespace DungeonsAndDungeons
{
    /// <summary>
    /// Represents a collection of Items
    /// </summary>
    public class Inventory
    {
        private List<Item> Items { get; set; }
        public int ItemLimit { get; set; }

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
