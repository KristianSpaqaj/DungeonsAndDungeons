using DungeonsAndDungeons.Entities;
using Microsoft.Xna.Framework;

namespace DungeonsAndDungeons.Commands
{
    public abstract class RotateCommand : Command
    {
        public override int ActionCost => 0;
        public abstract int Direction { get; }
        private Vector2 Rotation { get; }

        public RotateCommand(Entity entity, Level level, GameContext ctx) : base(entity, level, ctx)
        {
            Rotation = new Vector2(-Creator.Direction.Y, Creator.Direction.X);
        }

        public override void Execute()
        {
            Creator.Direction = Rotation * Direction;
            Creator.Angle = 90 * Direction;
        }
    }
}
