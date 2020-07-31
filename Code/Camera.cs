using DungeonsAndDungeons.Extensions;
using Microsoft.Xna.Framework;
using System;

namespace DungeonsAndDungeons
{
    public class Camera
    {
        private Vector2 direction;

        public Vector2 Position { get; set; }
        public Vector2 Direction { get => direction; private set => direction = value; }


        public Vector2 Plane { get; private set; }

        public Camera(Vector2 position, Vector2 direction, float FOV)
        {
            Position = position;
            Direction = direction;

            double radians = FOV * (Math.PI / 180);

            float p = (float)(Direction.Length() * Math.Tan(radians / 2));
            Plane = new Vector2((float)p, 0);

            if (Direction.X == 0)
            {
                Plane = new Vector2(p, 0);
            }
            else
            {
                Plane = new Vector2(0, p);
            }
        }

        /// <summary>
        /// Moves the camera forward according to it's nearest quadrant
        /// </summary>
        /// <param name="time"></param>
        /// <param name="forward"></param>
        public void Move(GameTime time, bool forward = true)
        {
            Position = Position + Direction;
        }

        /// <summary>
        /// Sets the camera's direction and plane vector to the rotation of the given vector
        /// </summary>
        /// <remarks>Is currently buggy</remarks>
        /// <param name="newDir"></param>
        public void SetDirection(Vector2 newDir)
        {
            double angle = Direction.GetAngleDegrees(newDir);

            Rotate((int)angle);
        }

        /// <summary>
        /// Rotates the camera's direction and plane vector by <c>degrees</c>
        /// </summary>
        /// <param name="degrees"></param>
        public void Rotate(int degrees)
        {
            Direction = Direction.RotateDegree(degrees);
            Plane = Plane.RotateDegree(degrees);
        }
    }
}
