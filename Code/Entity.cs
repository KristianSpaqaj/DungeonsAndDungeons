using DungeonsAndDungeons.Commands;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DungeonsAndDungeons
{
    abstract public class Entity
    {
        public int Id { get; }
        public List<Sprite> Stances { get; set; }
        private int StanceIndex { get; set; }
        public Sprite Sprite { get => Stances[StanceIndex]; }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public Inventory Inventory { get; set; }
        public double Health { get; set; }
        public EntityState State { get; set; }

        public Entity(Vector2 position, Vector2 direction, Inventory inventory, double health, List<Sprite> stance, EntityState state = EntityState.IDLE)
        {
            Position = position;
            Direction = direction;
            Inventory = inventory;
            Health = health;
            Stances = stance;
            StanceIndex = 0;
            State = state;
        }

        abstract public Command GetAction(Level level, GameContext ctx);

    }
}
