using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace DungeonsAndDungeons
{
    public class MapGenerator
    {
        const int MAX_LEAF_SIZE = 8;
        private List<Leaf> Leafs { get; set; }
        private Leaf Leaf { get; set; }
        private Random Random { get; }
        private int MapHeight { get; }
        private int MapWidth { get; }

        public MapGenerator(int height, int width)
        {
            Leafs = new List<Leaf>();
            Random = new Random();
            MapHeight = height;
            MapWidth = width;

            Generate();
        }

        public int[,] LeafsToTiles()
        {
            int[,] tiles = new int[MapHeight,MapWidth];
            foreach(Leaf leaf in Leafs)
            {
                for(int i = leaf.X; i < leaf.Width; i++)
                {
                    for(int j = leaf.Y; j < leaf.Height; j++)
                    {
                        if(i == 0 || i == leaf.Width-1)
                        {
                            tiles[i, j] = 1;
                        }else if(j == 0 || j == leaf.Height-1)
                        {
                            tiles[i, j] = 1;
                        }
                    }
                }
            }

            return tiles;
        }

        public void Generate()
        {
            Leaf root = new Leaf(0, 0, MapWidth, MapHeight);
            Leafs.Add(root);
            bool didSplit = true;

            while (didSplit)
            {
                didSplit = false;
                for(int i = 0; i < Leafs.Count; i++)
                {
                    Leaf l = Leafs[i];
                    if (l.LeftChild == null && l.RightChild == null) // if this Leaf is not already split...
                    {
                        // if this Leaf is too big, or 75% chance...
                        if (l.Width > MAX_LEAF_SIZE || l.Height > MAX_LEAF_SIZE || Random.Next(0,100) > 25)
                        {
                            if (l.Split()) // split the Leaf!
                            {
                                // if we did split, push the child leafs to the Vector so we can loop into them next
                                Leafs.Add(l.LeftChild);
                                Leafs.Add(l.RightChild);
                                didSplit = true;
                            }
                        }
                    }
                }
            }
        }
    }

    public class Leaf
    {
        private const int MIN_LEAF_SIZE = 6;
        public int Y { get; set; }
        public int X { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Leaf LeftChild { get; set; }
        public Leaf RightChild { get; set; }
        public Rectangle Room { get; set; }
        private Random Random { get; }
        public Leaf(int y, int x, int width, int height)
        {
            Y = y;
            X = x;
            Width = width;
            Height = height;
            Random = new Random();
        }

        public bool Split()
        {
            if (LeftChild != null || RightChild != null) // if leaf already split
            {
                return false;
            }

            // determine direction of split
            // if the width is >25% larger than height, we split vertically
            // if the height is >25% larger than the width, we split horizontally
            // otherwise we split randomly

            bool splitHorizontal = Random.Next(0, 100) > 50;

            if (Width > Height && Width / Height >= 1.25)
                splitHorizontal = false;
            else if (Height > Width && Height / Width >= 1.25)
                splitHorizontal = true;

            int max = (splitHorizontal ? Height : Width);

            if (max <= MIN_LEAF_SIZE)
            {
                return false; // area indivisible
            }

            int split = Random.Next(MIN_LEAF_SIZE, max);

            if (splitHorizontal)
            {
                LeftChild = new Leaf(X, Y, Width, split);
                RightChild = new Leaf(X, Y + split, Width, Height - split);
            }
            else
            {
                LeftChild = new Leaf(X, Y, split, Height);
                RightChild = new Leaf(X + split, Y, Width - split, Height);
            }
            return true; // split successful!
        }

    }
}
