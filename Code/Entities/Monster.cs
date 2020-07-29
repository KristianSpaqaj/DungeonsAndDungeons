using DungeonsAndDungeons.Commands;
using DungeonsAndDungeons.Extensions;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace DungeonsAndDungeons.Entities
{
    public class Monster : Entity
    {
        public Monster(Vector2 position, Vector2 direction, Inventory inventory, double health, List<Sprite> stances, EntityState state = EntityState.IDLE)
            : base(position, direction, inventory, health, stances, state)
        {
        }


        public override Command GetAction(Level level, GameContext ctx)
        {

            int nextX = (int)(Position.X + Direction.X);
            int nextY = (int)(Position.Y + Direction.Y);

            if (!level.Map.IsValid(nextX,nextY))
            {
                Random rand = new Random();
                Direction = Direction.RotateDegree(180);
            }

            return new MoveCommand(this, level, ctx, true);
        }

    }
}
