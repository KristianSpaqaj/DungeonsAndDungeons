using DungeonsAndDungeons.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonsAndDungeons
{
    /// <summary>
    /// Wrapper class for <c>Texture2D</c> which allows for implicit conversion to both <c>Color</c> arrays and <c>Texture2D</c>
    /// </summary>
    public class Sprite
    {
        public Color[] Pixels { get; set; }
        private readonly Texture2D Texture;

        public Sprite(Texture2D texture)
        {
            Pixels = texture.GenerateColorArray();
            Texture = texture;
        }

        public static implicit operator Texture2D(Sprite s) => s.Texture;
        public static implicit operator Color[](Sprite s) => s.Pixels;
    }
}
