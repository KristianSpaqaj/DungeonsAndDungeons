using DungeonsAndDungeons.Entities;
using Microsoft.Xna.Framework;

namespace DungeonsAndDungeons.Commands
{
    public class RotateCommand : Command
    {

        private int Direction { get; }
        private Vector2 Rotation { get; }

        public RotateCommand(Entity entity, Level level, GameContext ctx, bool turnRight) : base(entity, level, ctx)
        {
            Direction = turnRight ? -1 : 1;
            Rotation = new Vector2(-Creator.Direction.Y, Creator.Direction.X);
        }

        public override void Execute()
        {
            Creator.Direction = Rotation * Direction;
        }
    }
}
