﻿using DungeonsAndDungeons.Commands;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace DungeonsAndDungeons
{
    public class Monster : Entity
    {
        public Monster(Vector2 position, Vector2 direction, Inventory inventory, double health, List<Sprite> stances, EntityState state = EntityState.IDLE)
            : base(position, direction, inventory, health, stances, state)
        {
        }

        public override Command GetAction(Level level, GameContext ctx)
        {
            int nextY = (int)Math.Ceiling(Position.Y + 1);

            if(level.Map[(int)Position.X, nextY] != 0)
            {
                Direction = Direction.RotateDegree(180);
            }
             

            return new MoveCommand(this, level, ctx, true);
        }

    }
}
