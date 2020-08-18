using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsAndDungeons.Generation
{
    public class RoomLayout
    {
        private int[,] Map;

        public RoomLayout(int[,] map, int numberOfRooms)
        {
            Map = map;
            NumberOfRooms = numberOfRooms;
            MapHeight = Map.GetLength(0);
            MapWidth = Map.GetLength(1);
            Tiles = map;
        }

        public int[,] Tiles { get; set; }
        public int NumberOfRooms { get; set; }
        public int MapHeight { get; set; }
        public int MapWidth { get; set; }

        public override string ToString()
        {
            string s = "";
            for(int i = 0; i < MapHeight; i++)
            {
                for(int j = 0; j < MapWidth; j++)
                {
                    s += "[" + Tiles[i, j].ToString() + "]";
                }

                s += "\n";
            }

            return s;
        }

    }
}
