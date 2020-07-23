using System;
using DungeonsAndDungeons.Code;
using Microsoft.Xna.Framework;

namespace DungeonsAndDungeons
{
    public class Camera
    {
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public Vector2 Plane { get; set; }

        public Camera(Vector2 position, Vector2 direction, Vector2 plane)
        {
            Position = position;
            Direction = direction;
            Plane = plane;
        }

        

    }
}
