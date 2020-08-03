using DungeonsAndDungeons.Entities;

namespace DungeonsAndDungeons.Commands
{
    public class EmptyCommand : Command // for testing purposes only
    {
        public override int ActionCost => 0;

        public EmptyCommand(Entity entity, Level level, GameContext ctx) : base(entity, level, ctx)
        {
        }

        public override void Execute()
        {
        }
    }
}
