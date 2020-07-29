using DungeonsAndDungeons.Entities;
using System.Collections.Generic;
using System.Linq;

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


        public List<Item> ItemsAt(int x, int y)
        { // TODO generate item map with bools
            return Items.Where((i) => (int)i.Position.X == x && (int)i.Position.Y == y).ToList();
        }
    }
}
