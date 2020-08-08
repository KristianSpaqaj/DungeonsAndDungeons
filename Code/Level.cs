using DungeonsAndDungeons.Entities;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DungeonsAndDungeons
{
    public class Level
    {
        public Player Player { get; }
        public TexturedMap Map { get; }
        public List<Item> Items { get; }
        public List<Entity> Entities { get; }

        public Level(TexturedMap map, List<Item> items, List<Entity> entities, Player player)
        {
            Player = player;
            Map = map;
            Items = items;
            Entities = entities;
        }


        public List<Item> ItemsAt(int x, int y)
        { // TODO generate item map with bools
            return Items.Where((i) => (int)i.Position.X == x && (int)i.Position.Y == y).ToList();
        }

        public List<Item> ItemsAt(Vector2 position)
        {
            return ItemsAt((int)position.X, (int)position.Y);
        }
    }
}
