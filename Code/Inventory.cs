using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDungeons
{
    class Inventory<T>
    {
        private List<T> Items { get; set; }
        public int ItemLimit { get; set; }
        
        public void AddItem(T item)
        {
            Items.Add(item);
        }
        
        public T GetItem(int index)
        {
            return Items.ElementAt(index);
        }

        public List<T> GetAllItems()
        {
            return Items;
        }

        public void DropItem(int index)
        {
            Items.RemoveAt(index);
        }

    }
}
