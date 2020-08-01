using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DungeonsAndDungeons
{
    /// <summary>
    /// Represents a collection of Items
    /// </summary>
    public class Inventory : Collection<Item>
    {
        public Inventory() : base(new List<Item>(10)) { } // an inventory always has 10 slots

    }
}
