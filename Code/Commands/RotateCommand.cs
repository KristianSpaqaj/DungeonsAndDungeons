using DungeonsAndDungeons.Entities;
using DungeonsAndDungeons.Extensions;

namespace DungeonsAndDungeons.Commands
{
    public class RotateCommand : Command
    {

        public bool TurnRight { get; set; } = false;

        public RotateCommand(Entity entity, Level level, GameContext ctx, bool turnRight) : base(entity, level, ctx)
        {
            TurnRight = turnRight;
        }

        public override void Execute()
        {
            Creator.Direction = Creator.Direction.RotateDegree(90 * (TurnRight ? -1 : 1));
        }
    }
}
