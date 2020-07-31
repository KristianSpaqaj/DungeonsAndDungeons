using DungeonsAndDungeons.Commands;
using DungeonsAndDungeons.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DungeonsAndDungeons.Entities
{
    abstract public class Entity : IRenderable
    {
        public int Id { get; }
        public List<Sprite> Stances { get; }
        protected int StanceIndex { get; set; }
        public Sprite Sprite { get => Stances[StanceIndex]; }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public Inventory Inventory { get; }
        public Health Health { get; set; }
        public EntityState State { get; }

        public Entity(Vector2 position, Vector2 direction, Inventory inventory, Health health, List<Sprite> stance, EntityState state = EntityState.IDLE)
        {
            Position = position;
            Direction = direction;
            Inventory = inventory;
            Health = health;
            Stances = stance;
            StanceIndex = 0;
            State = state;
        }

        /// <summary>
        /// Used for updating the internal entity state, if such processing is required by a given entity
        /// </summary>
        /// <param name="level"></param>
        /// <param name="ctx"></param>
        virtual public void Update() { } // should perhaps be abstract

        /// <summary>
        /// Computes an command that represents the entity's action for the turn
        /// </summary>
        /// <param name="level"></param>
        /// <param name="ctx"></param>
        /// <returns>A Command object representing an action</returns>
        abstract public Command GetAction(Level level, GameContext ctx);
    }
}
