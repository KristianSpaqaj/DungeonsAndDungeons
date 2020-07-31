namespace DungeonsAndDungeons
{
    public class Health : Range<int>
    {
        public int Remaining { get; set; }

        public Health(int maximum) : base(0, maximum)
        {
            Remaining = maximum;
        }


    }
}
