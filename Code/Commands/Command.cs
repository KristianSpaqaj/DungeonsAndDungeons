namespace DungeonsAndDungeons.Commands
{
    public abstract class Command
    {
        protected Entity Creator { get; set; }
        protected Level Level { get; set; }
        protected GameContext Context { get; set; }

        public Command(Entity entity, Level level, GameContext ctx)
        {
            Creator = entity;
            Level = level;
            Context = ctx;
        }

        public abstract void Execute();
    }
}
