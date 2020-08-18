using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace DungeonsAndDungeons.Generation
{
    public class MapGenerator
    {
        public (int x, int y) StartingPoint { get; }
        public DungeonGenerator DungeonGenerator { get; }
        public MapGenerator(int numRooms, int roomSize)
        {
            RoomLayoutGenerator roomGen = new RoomLayoutGenerator(numRooms);
            DungeonGenerator = new DungeonGenerator(roomGen.Generate(), roomSize,roomSize);
            DungeonGenerator.Generate();
        }
    }
}
