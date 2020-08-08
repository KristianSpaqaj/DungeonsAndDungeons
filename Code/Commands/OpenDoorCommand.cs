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
            int nextX = (int)(Creator.Position.X + Creator.Direction.X);
            int nextY = (int)(Creator.Position.Y + Creator.Direction.Y);


            Level.Map[nextX,nextY] = 0;
        }

        public override bool CanRun()
        {
            int nextX = (int)(Creator.Position.X + Creator.Direction.X);
            int nextY = (int)(Creator.Position.Y + Creator.Direction.Y);

            return Level.Map[nextX,nextY] == 7;
        }
    }
}
