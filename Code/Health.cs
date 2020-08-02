using System;

namespace DungeonsAndDungeons
{
    public class Health : Range<int>
    {
   
        public Health(int maximum) : base(0, maximum)
        {
        }
    }
}
