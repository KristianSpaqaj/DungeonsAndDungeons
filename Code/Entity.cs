using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace DungeonsAndDungeons
{
    public class Entity
    {
        public int Id { get; }
        public List<Sprite> Animation { get; set;  }
        private int AnimationIndex { get; set; }
        public Sprite Sprite { get => Animation[AnimationIndex]; }

        public Vector2 Position { get; set; }

        public Entity(Vector2 position, List<Sprite> animation)
        {
            AnimationIndex = 0;
            Animation = animation;
            Position = position;
            //generate ID
        }

    }
}
