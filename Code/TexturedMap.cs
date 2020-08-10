using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonsAndDungeons
{
    public class TexturedMap : Map<int>
    {
        public List<Sprite> Textures { get; }

        public TexturedMap(int[,] tiles, List<Texture2D> textures) : base(tiles, 0,7)
        {
            Textures = new List<Sprite>();

            VerifyTextureSizeConsistency(textures);

            foreach (Texture2D texture in textures)
            {
                Textures.Add(new Sprite(texture));
            }
        }

        public Color[] GetTileTexture(int x, int y)
        {
            return Textures[Tiles[y, x]];
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

    }
}
