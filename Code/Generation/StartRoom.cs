using DungeonsAndDungeons.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;

namespace DungeonsAndDungeons.Generation
{
    public class StartRoom : Room
    {
        public StartRoom(int x, int y, int height, int width) : base(x, y, height, width)
        {
        }

        public override RoomType Type => RoomType.START_ROOM;

        public override List<Entity> SpawnEntities()
        {
            Player player = new Player(new Vector2(Center.x + 0.5f, Center.y + 0.5f),
                                      new Vector2(-1, 0),
                                      new Inventory(10,
                                      new Item[] { }),
                                      new Health(100),
                                      new List<Sprite>() { },
                                      new ActionPoints(2));

            return new List<Entity>() { player };
        }

        public override List<Item> SpawnItems()
        {
            return new List<Item>();
        }
    }
}