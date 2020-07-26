using Microsoft.Xna.Framework;

namespace DungeonsAndDungeons.Attributes
{
    public class Direction : Attribute
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Direction(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Direction(Vector2 vector)
        {
            X = vector.X;
            Y = vector.Y;
        }

        public Vector2 ToVector2()
        {
            return new Vector2(X, Y);
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }
    }
}
