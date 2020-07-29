using DungeonsAndDungeons.Extensions;
using Microsoft.Xna.Framework;
using System;

namespace DungeonsAndDungeons
{
    public class Camera
    {
        private Vector2 direction;

        public Vector2 Position { get; set; }
        public Vector2 Direction { get => direction; set => direction = value; }


        public Vector2 Plane { get; set; }

        public Camera(Vector2 position, Vector2 direction, Vector2 plane)
        {
            Position = position;
            Direction = direction;
            Plane = plane;
        }

        /// <summary>
        /// Moves the camera forward according to it's nearest quadrant
        /// </summary>
        /// <param name="time"></param>
        /// <param name="forward"></param>
        public void Move(GameTime time, bool forward = true)
        {
            double angle = GetDirectionQuadrant();

            int direction = forward ? 1 : -1;

            switch (angle)
            {
                case -180: // this angle is occasionally erroneously calculated 
                case 180:
                    Position = new Vector2(Position.X - direction, Position.Y);
                    break;

                case 0:
                    Position = new Vector2(Position.X + direction, Position.Y);
                    break;

                case 90:
                    Position = new Vector2(Position.X, Position.Y + direction);
                    break;

                case -90:
                    Position = new Vector2(Position.X, Position.Y - direction);
                    break;
            }
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

        /// <summary>
        /// Computes the nearest quadrant that the camera is facing
        /// </summary>
        /// <remarks>Gives weird values but works</remarks>
        /// <returns></returns>
        public double GetDirectionQuadrant()
        {
            double angle = Math.Atan2(Direction.Y, Direction.X) * 180 / Math.PI;
            return Math.Round(angle / 90) * 90;
        }

    }
}
