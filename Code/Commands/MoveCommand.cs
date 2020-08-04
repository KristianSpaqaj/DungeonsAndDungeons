using DungeonsAndDungeons.Entities;

namespace DungeonsAndDungeons.Commands
{
    public abstract class MoveCommand : Command
    {
        protected abstract int Direction { get; }

        public MoveCommand(Entity entity, Level level, GameContext ctx) : base(entity, level, ctx) { }

        public override void Execute()
        {
            Creator.Position += (Creator.Direction * Direction);
        }

        public override bool CanRun()
        {
            return base.CanRun() && Level.Map.IsValid(Creator.Position+(Creator.Direction*Direction));
        }
    }
}
