using DungeonsAndDungeons.Entities;

namespace DungeonsAndDungeons.Commands
{
    public class OpenDoorCommand : Command
    {
        public OpenDoorCommand(Entity entity, Level level, GameContext ctx) : base(entity, level, ctx)
        {
        }

        public override void Execute()
        {
            Level.Map[Creator.Position + Creator.Direction] = 0;
        }

        public override bool CanRun()
        {
            return Level.Map[Creator.Position + Creator.Direction] == 7;
        }
    }
}
