using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace DungeonsAndDungeons.Generation
{
    public class MapGenerator
    {
        const int MAX_LEAF_SIZE = 8;
        const int MIN_LEAF_SIZE = 4;
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
        }

        public int[,] LeafsToTiles()
        {
            int[,] tiles = new int[MapHeight, MapWidth];
            foreach (Leaf leaf in Leafs)
            {
                for (int i = leaf.Y; i < leaf.Height; i++)
                {
                    for (int j = leaf.X; j < leaf.Width; j++)
                    {
                        if (i == leaf.Y || i == leaf.Y + leaf.Height - 1 || j == leaf.X || j == leaf.X + leaf.Width - 1)
                        {
                            tiles[i, j] = 1;
                        }
                    }
                }
            }

            return tiles;
        }

        public void Generate(int depth = 3)
        {
            Leaf root = new Leaf(0, 0, MapWidth, MapHeight);
            Leafs.Add(root);

            for (int i = 0; i < depth; i++)
            {
                Leaf l = Leafs[i];
                if (l.LeftChild == null && l.RightChild == null) // if this Leaf is not already split...
                {
                    if (l.Split()) // split the Leaf!
                    {
                        // if we did split, push the child leafs to the Vector so we can loop into them next
                        Leafs.Add(l.LeftChild);
                        Leafs.Add(l.RightChild);
                    }
                }
            }
        }
    }

    public class Leaf
    {
        private const int MIN_LEAF_SIZE = 5;
        public int Y { get; set; }
        public int X { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Leaf LeftChild { get; set; }
        public Leaf RightChild { get; set; }
        public Rectangle Room { get; set; }
        private Random Random { get; }
        public Leaf(int x, int y, int width, int height)
        {
            Y = y;
            X = x;
            Width = width;
            Height = height;
            Random = new Random();
        }

        public bool Split()
        {
            bool done = false;
            while (!done)
            {
                bool vertical = Random.Next(0, 2) == 0;
                if (vertical && Width >= MIN_LEAF_SIZE)
                {
                    //Vertical
                    LeftChild = new Leaf(X, Y, Random.Next(MIN_LEAF_SIZE, Width), Height);
                    RightChild = new Leaf(X + LeftChild.Width, Y, Width - LeftChild.Width, LeftChild.Height);
                    if (LeftChild.Width / LeftChild.Height < 0.25 || RightChild.Width / RightChild.Height < 0.25)
                    {
                        return false;
                    }
                }
                else if (!vertical && Height >= MIN_LEAF_SIZE)
                {
                    //Horizontal

                    LeftChild = new Leaf(X, Y, Width, Random.Next(MIN_LEAF_SIZE, Height));
                    RightChild = new Leaf(X, Y + LeftChild.Height, Width, Height - LeftChild.Height);
                    if (LeftChild.Height / LeftChild.Width < 0.25 || RightChild.Height / RightChild.Width < 0.25)
                    {
                        continue;
                    }
                }
                else
                {
                    return false;
                }

                done = true;
            }

            return true;
        }
    }
}
