using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace DungeonsAndDungeons
{
    abstract public class Entity
    {
        public int Id { get; }
        public List<Sprite> Animation { get; set;  }
        private int AnimationIndex { get; set; }
        public Sprite Sprite { get => Animation[AnimationIndex]; }
        public Vector2 Position { get; set; }
        public Vector2 Direction { get; set; }
        public Inventory Inventory { get; set; }
        public double Health { get; set; }
        public EntityState State { get; set; }
     
        public Entity(Vector2 position, Vector2 direction, Inventory inventory, double health, EntityState state = EntityState.IDLE)
        {
            AnimationIndex = 0;
            Position = position;
            Direction = direction;
            Inventory = inventory;
            Health = health;
            State = state;
        }

        abstract public void Update(Level level, GameContext ctx);

    }
}
