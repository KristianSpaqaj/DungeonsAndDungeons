using DungeonsAndDungeons.Code;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonsAndDungeons
{
    public class Map
    {
        public int[,] Tiles { get; set; }
        public int Height { get; }
        public int Width { get; }
        public List<Color[]> Textures { get; set; }

        public Map(int[,] tiles, List<Texture2D> textures)
        {
            Tiles = tiles;
            Width = tiles.GetLength(1);
            Height = tiles.GetLength(0);

            Textures = new List<Color[]>();

            VerifyTextureSizeConsistency(textures);

            foreach (Texture2D texture in textures)
            {
                Textures.Add(texture.GenerateColorArray());
            }
        }

        public Color[] GetTileTexture(int x, int y)
        {
            return Textures[Tiles[x, y]];
        }

        private void VerifyTextureSizeConsistency(List<Texture2D> textures)
        {
            int height = textures[0].Height;
            int width = textures[0].Width;

            if (!textures.All((texture) => texture.Height == height && texture.Width == width))
            {
                throw new ArgumentException("Textures must all be same height and width");
            }
        }

        public int this[int i, int j] => Tiles[i, j];
    }
}
