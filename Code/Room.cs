using Microsoft.Xna.Framework;

namespace DungeonsAndDungeons
{
    public class Room
    {

        public int Height { get; }
        public int Width { get; }
        public int Left { get; }
        public int Right { get; }
        public int Top { get; }
        public int Bottom { get; }
        public Point DoorPosition { get; }

        public Room(int x, int y, int height, int width, Point doorPos)
        {
            Height = height;
            Width = width;
            Left = x;
            Right = x + width;
            Top = y;
            Bottom = y + height;
            DoorPosition = doorPos;
        }

        public bool Contains(int x, int y)
        {
            return x >= Left && x <= Right && y >= Top && y <= Bottom;
        }
    }
}