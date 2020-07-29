using DungeonsAndDungeons.Entities;

namespace DungeonsAndDungeons.Commands
{
    public abstract class Command
    {
        protected Entity Creator { get; private set; }
        protected Level Level { get; private set; }
        protected GameContext Context { get; private set; }

        public Command(Entity entity, Level level, GameContext ctx)
        {
            Creator = entity;
            Level = level;
            Context = ctx;
        }

        public abstract void Execute();
    }
}
