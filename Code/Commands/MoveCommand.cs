using DungeonsAndDungeons.Entities;
using Microsoft.Xna.Framework;
using System;

namespace DungeonsAndDungeons.Commands
{
    public class MoveCommand : Command
    {
        private int Direction { get; }
        public MoveCommand(Entity entity, Level level, GameContext ctx, bool moveForward) : base(entity, level, ctx)
        {
            Direction = (moveForward ? 1 : -1);
        }

        public override void Execute()
        {
            Move();
        }

        private void Move()
        {
            Creator.Position = Creator.Position + (Creator.Direction * Direction);
        }

    }
}
