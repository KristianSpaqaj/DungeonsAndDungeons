using DungeonsAndDungeons.Entities;
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
            throw new NotImplementedException();
        }

        public override List<Item> SpawnItems()
        {
            return new List<Item>();
        }
    }
}