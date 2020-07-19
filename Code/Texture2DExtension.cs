using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDungeons.Code
{
    public static class Texture2DExtension
    {
        public static Color[] GenerateColorArray(this Texture2D texture)
        {
            Color[] colors = new Color[texture.Width * texture.Height];
            texture.GetData<Color>(colors);
            return colors;
        }
    }
}
