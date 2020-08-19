using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace DungeonsAndDungeons.Generation
{
    public class MapGenerator
    {
        public (int x, int y) StartingPoint { get; }
        public LayoutParser LayoutParser { get; }
        public MapGenerator(int numRooms, int roomSize)
        {
            RoomLayoutGenerator roomGen = new RoomLayoutGenerator(numRooms);
            LayoutParser = new LayoutParser(roomGen.Generate(), roomSize,roomSize);
        }
    }
}
