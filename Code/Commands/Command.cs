using DungeonsAndDungeons.Entities;
using DungeonsAndDungeons.Interfaces;

namespace DungeonsAndDungeons.Commands
{
    public abstract class Command : ICommand
    {
        public Entity Creator { get; }
        public Level Level { get; }
        public GameContext Context { get; }
        public virtual int ActionCost => 1;
        public virtual bool TimesOut => false;

        public Command(Entity entity, Level level, GameContext ctx)
        {
            Creator = entity;
            Level = level;
            Context = ctx;
        }

        public abstract void Execute();
        public virtual bool CanRun() { return Creator.ActionPoints >= ActionCost; }
    }
}
