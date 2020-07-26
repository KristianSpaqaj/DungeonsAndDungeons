using System;
using Microsoft.Xna.Framework;

namespace DungeonsAndDungeons.Code
{
    public static class Vector2Extensions
    {
        public static Vector2 RotateDegree(this Vector2 vector, int degrees)
        {
            double radians = degrees * (Math.PI / 180);
            return RotateInRadians(vector, radians);
        }

        public static Vector2 RotateInRadians(this Vector2 vector, double radians)
        {

            float cosAngle = (float)Math.Cos(radians);
            float sinAngle = (float)Math.Sin(radians);

            return new Vector2(cosAngle * vector.X - sinAngle * vector.Y,
                               sinAngle * vector.X + cosAngle * vector.Y);

        }


        public static float GetQuadrant(this Vector2 vector)
        {
            double angle = Math.Atan2(vector.Y, vector.X) * 180 / Math.PI;
            return 0;            //return Math.Round(angle / 90) * 90;
        }
    }
}
