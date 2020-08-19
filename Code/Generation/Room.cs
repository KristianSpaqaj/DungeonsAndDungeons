using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace DungeonsAndDungeons.Generation
{
    class Room
    {
        public int Type { get; }
        public int Left { get; }
        public int Right { get; }
        public int Top { get; }
        public int Bottom { get; }
        public (int x, int y) Center { get; }

        public Room(int x, int y, int height, int width, int type)
        {
            Left = x;
            Right = x + width;
            Top = y;
            Bottom = y + height;
            Center = (Left + width / 2, Top + height / 2);
            Type = type;
        }
    }
}
