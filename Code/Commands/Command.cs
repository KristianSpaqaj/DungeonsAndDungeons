using DungeonsAndDungeons.Entities;

namespace DungeonsAndDungeons.Commands
{
    public abstract class Command
    {
        protected Entity Creator { get; }
        protected Level Level { get; }
        protected GameContext Context { get; }
        public int ActionCost { get; }

        public Command(Entity entity, Level level, GameContext ctx, int actionCost = 1)
        {
            Creator = entity;
            Level = level;
            Context = ctx;
            ActionCost = actionCost;
        }

        public abstract void Execute();
    }
}
