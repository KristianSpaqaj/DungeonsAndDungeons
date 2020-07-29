using DungeonsAndDungeons.Entities;

namespace DungeonsAndDungeons.Commands
{
    public abstract class Command
    {
        protected Entity Creator { get; }
        protected Level Level { get; }
        protected GameContext Context { get; }

        public Command(Entity entity, Level level, GameContext ctx)
        {
            Creator = entity;
            Level = level;
            Context = ctx;
        }

        public abstract void Execute();
    }
}
