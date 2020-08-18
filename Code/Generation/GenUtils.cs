using System;
using System.Collections.Generic;
using System.Text;

namespace DungeonsAndDungeons.Generation
{
    static class GenUtils
    {
        public static int[,] CreateEmptyMap(int height, int width, int defaultTile = 0)
        {
            int[,] ret = new int[height, width];
            for (int i = 0; i < width * height; i++) ret[i % width, i / width] = defaultTile;
            return ret;
        }
    }
}
