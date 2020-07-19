using DungeonsAndDungeons.Code;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDungeons
{
    public class Sprite
    {
        public Color[] Texture { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }

        public Sprite(Texture2D texture, float posX, float posY)
        {
            Texture = texture.GenerateColorArray();
            PosX = posX;
            PosY = posY;
        }
    }
}
