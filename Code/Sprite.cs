using DungeonsAndDungeons.Extensions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonsAndDungeons
{
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
