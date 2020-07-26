namespace DungeonsAndDungeons
{
    public class Behaviour
    {
        public virtual Command Run(int callerId, Level level)
        {
            return new Command();
        }
    }
}