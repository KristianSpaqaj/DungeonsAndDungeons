using System.Collections.Generic;

namespace DungeonsAndDungeons
{
    class Level
    {
        public Map Map { get; set; }
        public List<Item> Item { get; set; }

        public List<Entity> Entities { get; set; }
    }
}
