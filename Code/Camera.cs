using System;
using System.IO;
using DungeonsAndDungeons.Code;
using Microsoft.Xna.Framework;

namespace DungeonsAndDungeons
{
    public class Camera
    {
        private Vector2 plane;
        private Vector2 direction;

        public Vector2 Position { get; set; }
        public Vector2 Direction { get => direction; set => direction = value; }
        public Vector2 Plane { get => plane;  set => plane = value; }

        public Camera(Vector2 position, Vector2 direction, Vector2 plane)
        {
            Position = position;
            Direction = direction;
            Plane = plane;
        }
        public void SetDirection(Vector2 newRot)
        {
            int angleDelta = (int)((180/Math.PI) * Math.Acos(Vector2.Dot(direction, newRot)));

            direction = direction.RotateDegree(angleDelta);
            plane = plane.RotateDegree(angleDelta);
        }

    }

}
