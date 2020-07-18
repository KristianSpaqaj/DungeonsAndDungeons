using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDungeons
{
    public class Map
    {
        public int[,] Tiles { get; set; }
        public int Height { get; }
        public int Width { get; }

        public Map(int[,] tiles)
        {
            Tiles = tiles;
            Width = tiles.GetLength(1);
            Height = tiles.GetLength(0);
        }

        // Tilføj Textures

        //Tilføj GetTexture

        public int this[int i, int j] => Tiles[i,j];
    }
}
