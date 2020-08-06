using DungeonsAndDungeons.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DungeonsAndDungeons.Entities
{
    class Dude : Entity
    {
        public Dude(Vector2 position, Vector2 direction, Inventory inventory, Health health, List<Sprite> stance, ActionPoints ap, EntityState state = EntityState.IDLE) : base(position, direction, inventory, health, stance, ap, state)
        {
        }

        public override ICommand GetAction(Level level, GameContext ctx)
        {
            return null;
        }
    }
}
