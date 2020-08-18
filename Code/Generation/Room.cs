using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DungeonsAndDungeons.Generation
{
    class Room
    {
        public (int y,int x) Position { get; set; }
        public (int y, int x) Size { get; set; }
        public int Type { get; }

        public Room((int y, int x) position, (int y, int x) size, int type)
        {
            Position = position;
            Size = size;
            Type = type;
        }
    }
}
