using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsAndDungeons.Generation
{
    public class RoomLayout
    {
        private int[,] Map;
        public int[,] Tiles { get; set; }
        public int NumberOfRooms { get; set; }
        public int MapHeight { get; set; }
        public int MapWidth { get; set; }

        public RoomLayout(int[,] map, int numberOfRooms)
        {
            Map = map;
            NumberOfRooms = numberOfRooms;
            MapHeight = Map.GetLength(0);
            MapWidth = Map.GetLength(1);
            Tiles = map;
        }
    }
}
