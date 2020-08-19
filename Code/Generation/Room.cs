﻿using DungeonsAndDungeons.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace DungeonsAndDungeons.Generation
{
    public abstract class Room
    {
        public int Type { get; }
        public int Left { get; }
        public int Right { get; }
        public int Top { get; }
        public int Bottom { get; }
        public (int x, int y) Center { get; }
        private ItemGenerator ItemGenerator { get; }

        public Room(int x, int y, int height, int width, int type)
        {
            Left = x;
            Right = x + width;
            Top = y;
            Bottom = y + height;
            Center = (Left + width / 2, Top + height / 2);
            Type = type;

            ItemGenerator = new ItemGenerator();
        }

        public bool Contains(int x, int y)
        {
            return x >= Left && x <= Right && y >= Top && y <= Bottom;
        }

        public Point RandomPosition()
        {
            Random r = new Random();
            int randX = r.Next(Left, Right-1);
            int randY = r.Next(Top, Bottom-1);

            return new Point(randX, randY);
        }

        public List<Item> SpawnItems(int count)
        {
            return ItemGenerator.Generate(this, count);
        }

    }
}
