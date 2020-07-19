using DungeonsAndDungeons.Code;
using Microsoft.Xna.Framework;
using System;

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

        public void Move(GameTime time, bool forward=true)
        {
            double angle = GetDirectionQuadrant();

            int direction = forward ? 1 : -1;

            switch (angle)
            {
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

        public void Rotate(int degrees)
        {
            Direction = Direction.RotateDegree(degrees);
            Plane = Plane.RotateDegree(degrees);
        }

        private double GetDirectionQuadrant()
        {
            double angle = Math.Atan2(Direction.Y, Direction.X) * 180 / Math.PI;
            return Math.Round(angle / 90) * 90;
        }

    }
}
