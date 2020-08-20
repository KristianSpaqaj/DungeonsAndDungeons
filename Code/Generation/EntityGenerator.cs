using DungeonsAndDungeons.Entities;
using DungeonsAndDungeons.Generation;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDungeons.Generation
{
    public class EntityGenerator : IGenerator<Entity>
    {
        public List<Entity> Generate(Room room, int count)
        {
            List<Entity> entities = new List<Entity>();
            Monster prototype = new Monster(new Vector2(8.5f, 2.5f),
                                new Vector2(-1, 0),
                                new Inventory(10),
                                new Health(100),
                                new List<Sprite>() { new Sprite(ContentContainer.Manager.Load<Texture2D>("demon")) },
                                new ActionPoints(2));

            for (int i = 0; i < count; i++)
            {
                Point position = room.RandomPosition();
                entities.Add(new Monster(new Vector2(position.X + 0.5f, position.Y + 0.5f), prototype.Direction, prototype.Inventory, prototype.Health, prototype.Stances, prototype.ActionPoints));
            }

            return entities;
        }
    }
}
