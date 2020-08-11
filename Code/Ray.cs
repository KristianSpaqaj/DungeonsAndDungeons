using Microsoft.Xna.Framework;
using System;

namespace DungeonsAndDungeons
{
    public class Ray
    {
        public double DirectionX { get; }
        public double DirectionY { get; }
        public double OriginX { get; }
        public double OriginY { get; }
        // which direction to step in  x or y direction (either -1 or +1)
        public int StepX => DirectionX < 0 ? -1 : 1;
        public int StepY => DirectionY < 0 ? -1 : 1;
        public double DeltaDistX => (DirectionX == 0) ? 0 : ((DirectionX == 0) ? 1 : Math.Abs(1 / DirectionX));
        public double DeltaDistY => (DirectionY == 0) ? 0 : ((DirectionY == 0) ? 1 : Math.Abs(1 / DirectionY));
        public int MapX { get; private set; }
        public int MapY { get; private set; }
        public int Side { get; set; } // NS or EW wall hit?
        public double SideDistX { get; set; }
        public double SideDistY { get; set; }

        public Ray(float originX, float originY, float directionX, float directionY)
        {
            OriginX = originX;
            OriginY = originY;
            DirectionX = directionX;
            DirectionY = directionY;
            MapX = (int)OriginX;
            MapY = (int)OriginY;

            SideDistX = (DirectionX < 0) ? (OriginX - MapX) * DeltaDistX : (MapX + 1.0 - OriginX) * DeltaDistX;
            SideDistY = (DirectionY < 0) ? (OriginY - MapY) * DeltaDistY : (MapY + 1.0 - OriginY) * DeltaDistY;

        }

        public Ray(Vector2 OriginPosition, Vector2 Direction) : this(OriginPosition.X, OriginPosition.Y, Direction.X, Direction.Y) { }

        public void GoToNextSquare()
        {
            if (SideDistX < SideDistY)
            {
                SideDistX += DeltaDistX;
                MapX += StepX;
                Side = 0;
            }
            else
            {
                SideDistY += DeltaDistY;
                MapY += StepY;
                Side = 1;
            }
        }

        public double GetDistance()
        {
            if(Side == 0)
            {
                return (MapX - OriginX + (1 - StepX) / 2) / DirectionX;
            }
            else
            {
                return (MapY - OriginY + (1 - StepY) / 2) / DirectionY;
            }
        }

    }
}
