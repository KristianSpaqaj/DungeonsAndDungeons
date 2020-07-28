using System.Collections.Generic;

namespace DungeonsAndDungeons
{
    public class Level
    {
        public Player Player { get; set; }
        public Map Map { get; set; }
        public List<Item> Item { get; set; }
        public List<Entity> Entities { get; set; }

        public Level(Map map, List<Item> item, List<Entity> entities)
        {
            Map = map;
            Item = item;
            Entities = entities;
        }

        public Level(Map map, List<Item> item, List<Entity> entities, Player player) : this(map, item, entities)
        {
            Player = player;
        }
    }
}
