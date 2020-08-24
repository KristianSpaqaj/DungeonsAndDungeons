using DungeonsAndDungeons.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace DungeonsAndDungeons.Generation
{
    public abstract class Room
    {
        public abstract RoomType Type { get; }
        public int Left { get; private set; }
        public int Right { get; private set; }
        public int Top { get; private set; }
        public int Bottom { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }
        public (int x, int y) Center { get; private set; }
        protected ItemGenerator ItemGenerator { get; }
        protected EntityGenerator EntityGenerator { get; }

        public Room(int x, int y, int height, int width)
        {
            Left = x;
            Right = x + width;
            Top = y;
            Bottom = y + height;
            Center = (Left + width / 2, Top + height / 2);
            Height = height;
            Width = width;
            ItemGenerator = new ItemGenerator();
            EntityGenerator = new EntityGenerator();
        }

        public bool Contains(int x, int y)
        {
            return x >= Left && x <= Right && y >= Top && y <= Bottom;
        }

        public void Inflate(int x, int y)
        {
            Top -= y;
            Left -= x;
            Right += x ;
            Bottom += y;
            Center = (Left + Width / 2, Top + Height / 2);
        }

        public Point RandomPosition()
        {
            Random r = new Random();
            int randX = r.Next(Left, Right - 1);
            int randY = r.Next(Top, Bottom - 1);

            return new Point(randX, randY);
        }
        public bool IntersectsWith(Room room)
        {
            return Left < room.Right && Right > room.Left && Top > room.Bottom && Bottom < room.Top;
        }

        public abstract List<Item> SpawnItems();
        public abstract List<Entity> SpawnEntities();
    }
}
