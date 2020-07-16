using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDungeons
{
    class Weapon
    {
        public double Damage { get; set; }
        public int Range { get; set; }
        public Dictionary<string, double> Attributes { get; set; }
    }
}
