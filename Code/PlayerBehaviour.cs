using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonsAndDungeons.Attributes;
using Microsoft.Xna.Framework;

namespace DungeonsAndDungeons
{
    public class PlayerBehaviour : Behaviour
    {
        public override void Run(Entity caller, Level level, GameTime gameTime)
        {
            if (InputState.HasAction("W"))
            {
                Move(caller, gameTime);
            }
            if (InputState.HasAction("S"))
            {
                Move(caller, gameTime, false);
            }
            if (InputState.HasAction("A"))
            {
                Rotate(caller, 90);
            }
            if (InputState.HasAction("D"))
            {
                Rotate(caller, -90);
            }
        }

        public void Move(Entity entity, GameTime time, bool forward = true)
        {
            double angle = GetDirectionQuadrant(entity);

            int direction = forward ? 1 : -1;

            Position Position = entity.GetAttribute<Position>();


            switch (angle)
            {
                case 180:
                    Position = new Position(Position.X - direction, Position.Y);
                    break;

                case 0:
                    Position = new Position(Position.X + direction, Position.Y);
                    break;

                case 90:
                    Position = new Position(Position.X, Position.Y + direction);
                    break;

                case -90:
                    Position = new Position(Position.X, Position.Y - direction);
                    break;
            }

            entity.SetAttribute<Position>(Position);

        }

        private void Rotate(Entity entity, int degrees)
        {
            Vector2 dir = entity.GetAttribute<Direction>().ToVector2().RotateDegree(degrees);
            entity.SetAttribute<Direction>(new Direction(dir));
        }

        private double GetDirectionQuadrant(Entity entity)
        {
            Direction direction = entity.GetAttribute<Direction>();

            double angle = Math.Atan2(direction.Y, direction.X) * 180 / Math.PI;
            return Math.Round(angle / 90) * 90;
        }

    }
}
