﻿using Microsoft.Xna.Framework;

namespace DungeonsAndDungeons.Attributes
{
    public class Position : Attribute
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Position(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }
    }
}
