namespace DungeonsAndDungeons.Commands
{
    public class EmptyCommand : Command // for testing purposes only
    {
        public EmptyCommand() : base(null, null, null)
        {
            ActionCost = 0;
        }

        public override void Execute()
        {
        }
    }
}
