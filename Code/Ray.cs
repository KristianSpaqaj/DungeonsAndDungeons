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
        public int StepX { get; }
        public int StepY { get; } 
        public double DeltaDistX { get; }
        public double DeltaDistY { get; } 
        public int CurrentX { get; private set; }
        public int CurrentY { get; private set; }
        public int Side { get; set; } // NS or EW wall hit?
        public double SideDistX { get; set; }
        public double SideDistY { get; set; }

        public Ray(float originX, float originY, float directionX, float directionY)
        {
            OriginX = originX;
            OriginY = originY;
            DirectionX = directionX;
            DirectionY = directionY;
            CurrentX = (int)OriginX;
            CurrentY = (int)OriginY;

            StepX = DirectionX < 0 ? -1 : 1;
            StepY = DirectionY < 0 ? -1 : 1;
            DeltaDistX = (DirectionX == 0) ? 0 : ((DirectionX == 0) ? 1 : Math.Abs(1 / DirectionX));
            DeltaDistY = (DirectionY == 0) ? 0 : ((DirectionY == 0) ? 1 : Math.Abs(1 / DirectionY));
            SideDistX = (DirectionX < 0) ? (OriginX - CurrentX) * DeltaDistX : (CurrentX + 1.0 - OriginX) * DeltaDistX;
            SideDistY = (DirectionY < 0) ? (OriginY - CurrentY) * DeltaDistY : (CurrentY + 1.0 - OriginY) * DeltaDistY;


        }

        public Ray(Vector2 OriginPosition, Vector2 Direction) : this(OriginPosition.X, OriginPosition.Y, Direction.X, Direction.Y) { }

        public void GoToNextSquare()
        {
            if (SideDistX < SideDistY)
            {
                SideDistX += DeltaDistX;
                CurrentX += StepX;
                Side = 0;
            }
            else
            {
                SideDistY += DeltaDistY;
                CurrentY += StepY;
                Side = 1;
            }
        }

        public double GetDistanceTraveled()
        {
            if(Side == 0)
            {
                return (CurrentX - OriginX + (1 - StepX) / 2) / DirectionX;
            }
            else
            {
                return (CurrentY - OriginY + (1 - StepY) / 2) / DirectionY;
            }
        }

    }
}
