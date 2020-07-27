using DungeonsAndDungeons.Commands;
using Microsoft.Xna.Framework;
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
            return new EmptyCommand();
        }

    }
}
