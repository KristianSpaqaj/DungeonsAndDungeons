using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
