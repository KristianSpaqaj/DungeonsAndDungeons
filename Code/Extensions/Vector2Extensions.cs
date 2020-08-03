using Microsoft.Xna.Framework;
using System;

namespace DungeonsAndDungeons.Extensions
{
    public static class Vector2Extensions
    {
        public static Vector2 RotateDegree(this Vector2 vector, double degrees)
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

        public static double GetAngleDegrees(this Vector2 v1, Vector2 v2)
        {
            return (int)((180 / Math.PI) * Math.Acos(Vector2.Dot(v1, v2)));
        }
    }
}
