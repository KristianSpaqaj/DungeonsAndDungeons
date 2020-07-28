using System.Collections.Generic;

namespace DungeonsAndDungeons
{
    public class Weapon
    {
        public double Damage { get; set; }
        public int Range { get; set; }
        public Dictionary<string, double> Attributes { get; set; }
    }
}
