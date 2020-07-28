using Microsoft.Xna.Framework;
using System;

namespace DungeonsAndDungeons.Commands
{
    public class MoveCommand : Command
    {
        private bool MoveForward { get; set; }
        public MoveCommand(Entity entity, Level level, GameContext ctx, bool moveForward) : base(entity, level, ctx)
        {
            MoveForward = moveForward;
        }

        public override void Execute()
        {
            Move();
        }

        private void Move()
        {
            double angle = GetDirectionQuadrant();

            int direction = (MoveForward ? 1 : -1);

            switch (angle)
            {
                case -180: // this angle is occasionally erroneously calculated 
                case 180:
                    Creator.Position = new Vector2(Creator.Position.X - direction, Creator.Position.Y);
                    break;

                case 0:
                    Creator.Position = new Vector2(Creator.Position.X + direction, Creator.Position.Y);
                    break;

                case 90:
                    Creator.Position = new Vector2(Creator.Position.X, Creator.Position.Y + direction);
                    break;

                case -90:
                    Creator.Position = new Vector2(Creator.Position.X, Creator.Position.Y - direction);
                    break;
            }
        }

        private double GetDirectionQuadrant()
        {
            double angle = Math.Atan2(Creator.Direction.Y, Creator.Direction.X) * 180 / Math.PI;
            return Math.Round(angle / 90) * 90;
        }
    }
}
