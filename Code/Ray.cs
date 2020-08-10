using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDungeons
{
    public class Ray
    {
        public double DirectionX { get; set; }
        public double DirectionY { get; set; }
        // which direction to step in  x or y direction (either -1 or +1)
        public int StepX => DirectionX < 0 ? -1 : 1;
        public int StepY => DirectionY < 0 ? -1 : 1;
        public double SideDistX { get; set; }
        public double SideDistY { get; set; }
        public double DeltaDistX => (DirectionX == 0) ? 0 : ((DirectionX == 0) ? 1 : Math.Abs(1 / DirectionX));
        public double DeltaDistY => (DirectionY == 0) ? 0 : ((DirectionY == 0) ? 1 : Math.Abs(1 / DirectionY));
        public int MapX { get; set; }
        public int MapY { get; set; }
        public int Side { get; set; } // NS or EW wall hit?

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

    }

    public enum Axes
    {
        HORIZONTAL,
        VERITCAL
    }
}
