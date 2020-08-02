using System;

namespace DungeonsAndDungeons
{
    public class Health : Range<int>
    {
        private int _remaining;
        public int Remaining { get => _remaining; set => SetRemaining(value); }

        public Health(int maximum) : base(0, maximum)
        {
            if (!IsValid())
            {
                throw new ArgumentException("HP must be greater than 0");
            }

            Remaining = maximum;
        }

        private void SetRemaining(int value)
        {
            if(value < Minimum)
            {
                _remaining = Minimum;
            }else if(value > Maximum)
            {
                _remaining = Maximum;
            }
            else
            {
                _remaining = value;
            }
        }
    }
}
