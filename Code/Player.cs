using DungeonsAndDungeons.Code;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace DungeonsAndDungeons
{
    public class Player : Entity
    {
        public int Rotation { get; set; } // used for camera. TODO get rid of this
        public Player(Vector2 position, Vector2 direction, Inventory inventory, double health, List<Sprite> stance, EntityState state = EntityState.IDLE) : base(position, direction, inventory, health, stance, state)
        {
            Rotation = 0;
        }

        public override void Update(Level level, GameContext ctx)
        {
            Rotation = 0;
            if (InputState.HasAction("W"))
            {
                Move(ctx.GameTime);
            }
            if (InputState.HasAction("S"))
            {
                Move(ctx.GameTime, false);
            }
            if (InputState.HasAction("A"))
            {
                Direction = Direction.RotateDegree(90);
                Rotation = 90;
            }
            if (InputState.HasAction("D"))
            {
                Direction = Direction.RotateDegree(-90);
                Rotation = -90;
            }
        }

        public void Move(GameTime time, bool forward = true)
        {
            double angle = GetDirectionQuadrant();

            int direction = forward ? 1 : -1;

            switch (angle)
            {
                case -180: // this angle is occasionally erroneously calculated 
                case 180:
                    Position = new Vector2(Position.X - direction, Position.Y);
                    break;

                case 0:
                    Position = new Vector2(Position.X + direction, Position.Y);
                    break;

                case 90:
                    Position = new Vector2(Position.X, Position.Y + direction);
                    break;

                case -90:
                    Position = new Vector2(Position.X, Position.Y - direction);
                    break;
            }
        }

        public double GetDirectionQuadrant()
        {
            double angle = Math.Atan2(Direction.Y, Direction.X) * 180 / Math.PI;
            return Math.Round(angle / 90) * 90;
        }
    }
}