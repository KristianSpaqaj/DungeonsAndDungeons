﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDungeons
{
    class Inventory
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
