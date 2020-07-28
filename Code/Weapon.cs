using System.Collections.Generic;

namespace DungeonsAndDungeons
{
    public class Weapon : Item
    {
        public double Damage { get; set; }
        public int Range { get; set; }

        public Weapon(Dictionary<string, object> attributes, Sprite sprite, double damage, int range) : base(attributes, sprite)
        {
            Damage = damage;
            Range = range;
        }


    }
}
