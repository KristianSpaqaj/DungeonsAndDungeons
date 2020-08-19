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
        public Item Generate(Room room)
        {
            List<Item> items = new List<Item>();
            List<Sprite> sprites = new List<Sprite>
            {
                new Sprite(ContentContainer.Manager.Load<Texture2D>("key")),
                new Sprite(ContentContainer.Manager.Load<Texture2D>("knife"))
            };

            int spriteIndex = new Random().Next(0, sprites.Count);

            for (int i = 0; i < 1; i++)
            {
                Point position = room.RandomPosition();
                items.Add(new Item(sprites[spriteIndex], new Vector2(position.X+0.5f,position.Y+0.5f)));
            }

            return items[0];
        }
    }
}
