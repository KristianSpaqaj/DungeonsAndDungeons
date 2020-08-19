using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDungeons.Generation
{
    class ItemGenerator : IGenerator<Item>
    {
        public List<Sprite> Sprites { get; }
        public ItemGenerator()
        {
            Sprites = new List<Sprite>
            {
                new Sprite(ContentContainer.Manager.Load<Texture2D>("key")),
                new Sprite(ContentContainer.Manager.Load<Texture2D>("knife"))
            };
        }

        public List<Item> Generate(Room room, int count)
        {
            List<Item> items = new List<Item>();

            int spriteIndex = new Random().Next(0, Sprites.Count);

            for (int i = 0; i < count; i++)
            {
                Point position = room.RandomPosition();
                items.Add(new Item(Sprites[spriteIndex], new Vector2(position.X+0.5f,position.Y+0.5f)));
            }

            return items;
        }
    }
}
