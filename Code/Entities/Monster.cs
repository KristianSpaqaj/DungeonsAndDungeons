using DungeonsAndDungeons.Commands;
using DungeonsAndDungeons.Extensions;
using Microsoft.Xna.Framework;
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
            Vector2 nextPos = Vector2.Normalize(Position) * Vector2.Normalize(Direction);

            if (level.Map[(int)nextPos.X, (int)nextPos.Y] != 0)
            {
                Direction = Direction.RotateDegree(180);
            }


            return new MoveCommand(this, level, ctx, true);
        }

    }
}
