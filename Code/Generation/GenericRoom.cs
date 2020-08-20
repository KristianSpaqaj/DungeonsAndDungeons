using DungeonsAndDungeons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDungeons.Generation
{
    public class GenericRoom : Room
    {
        public GenericRoom(int x, int y, int height, int width) : base(x, y, height, width)
        {
        }

        public override RoomType Type => RoomType.GENERIC_ROOM;

        public override List<Entity> SpawnEntities()
        {
            return EntityGenerator.Generate(this, 1);
        }

        public override List<Item> SpawnItems()
        {
            Random random = new Random();
            return ItemGenerator.Generate(this,random.Next(0,2));
        }
    }
}
