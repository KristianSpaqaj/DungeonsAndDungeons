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
            Level.Map[Creator.Position + Creator.Direction] = Level.Map.EmptyTile;
        }

        public override bool CanRun()
        {
            return Level.Map[Creator.Position + Creator.Direction] == Level.Map.DoorTile;
        }
    }
}
