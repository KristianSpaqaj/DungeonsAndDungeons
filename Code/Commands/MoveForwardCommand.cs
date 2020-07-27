﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDungeons.Commands
{
    class MoveForwardCommand : Command
    {
        public MoveForwardCommand(Entity entity, Level level, GameContext ctx) : base(entity, level, ctx)
        {
        }

        public override void Execute()
        {
            Move();
        }

        private void Move()
        {
            double angle = GetDirectionQuadrant();

            int direction = 1;

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
