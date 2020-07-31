using DungeonsAndDungeons.Commands;

namespace DungeonsAndDungeons.Entities
{
    internal class HealthDownCommand : Command
    {
        private int v;
        public HealthDownCommand(Entity entity, Level level, GameContext ctx, int v) : base(entity,level,ctx)
        {
            this.v = v;
        }

        public override void Execute()
        {
            Creator.Health.Remaining -= v;
        }
    }
}