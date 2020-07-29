using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DungeonsAndDungeons
{
    public class Weapon : Item
    {
        public double Damage { get; set; }
        public int Range { get; set; }

        public Weapon(Sprite sprite, Vector2 position, double damage, int range, Dictionary<string, object> attributes = default) : base(sprite, position, attributes)
        {
            Damage = damage;
            Range = range;
        }
    }
}
