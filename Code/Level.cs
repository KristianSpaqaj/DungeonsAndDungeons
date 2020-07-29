using DungeonsAndDungeons.Entities;
using System.Collections.Generic;

namespace DungeonsAndDungeons
{
    public class Level
    {
        public Player Player { get; set; }
        public Map Map { get; set; }
        public List<Item> Items { get; set; }
        public List<Entity> Entities { get; set; }

        public Level(Map map, List<Item> items, List<Entity> entities)
        {
            Map = map;
            Items = items;
            Entities = entities;
        }

        public Level(Map map, List<Item> items, List<Entity> entities, Player player) : this(map, items, entities)
        {
            Player = player;
        }
    }
}
