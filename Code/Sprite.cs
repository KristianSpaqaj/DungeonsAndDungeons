using DungeonsAndDungeons.Code;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonsAndDungeons
{
    public class Sprite
    {
        public Color[] Pixels { get; set; }
        private Texture2D Texture;

        public Sprite(Texture2D texture, float posX, float posY)
        {
            Pixels = texture.GenerateColorArray();
            Texture = texture;
        }

        public static implicit operator Texture2D(Sprite s) => s.Texture;
        public static implicit operator Color[](Sprite s) => s.Pixels;
    }
}
