using DungeonsAndDungeons.Commands;

namespace DungeonsAndDungeons.Entities
{
    internal class HealthDownCommand : Command // NOTE only for test purposes
    {
        private readonly int v;
        public HealthDownCommand(Entity entity, Level level, GameContext ctx, int v) : base(entity, level, ctx)
        {
            this.v = v;
        }

        public override void Execute()
        {
            Creator.Health.Remaining -= v;
        }
    }
}