using DungeonsAndDungeons.Commands;
using DungeonsAndDungeons.Extensions;
using DungeonsAndDungeons.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DungeonsAndDungeons.Entities
{
    public class Monster : Entity
    {
        public Monster(Vector2 position, Vector2 direction, Inventory inventory, Health health, List<Sprite> stances, ActionPoints ap, EntityState state = EntityState.IDLE)
            : base(position, direction, inventory, health, stances, ap, state)
        {
        }


        public override ICommand GetAction(Level level, GameContext ctx)
        {
            if (!level.Map.IsValid(Position + Direction))
            {
                Direction = Direction.RotateDegree(180);
            }

            return new MoveForwardCommand(this, level, ctx);
        }

    }
}
