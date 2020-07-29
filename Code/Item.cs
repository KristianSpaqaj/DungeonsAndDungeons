using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DungeonsAndDungeons
{
    public class Item
    {
        public Dictionary<string, object> Attributes { get; set; }
        public Sprite Sprite { get; set; }
        public Vector2 Position { get; set; }

        public Item(Sprite sprite, Vector2 position, Dictionary<string, object> attributes = default)
        {
            Sprite = sprite;
            Position = position;
            Attributes = attributes;
        }
    }
}
