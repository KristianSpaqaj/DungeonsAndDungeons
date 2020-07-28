using System.Collections.Generic;

namespace DungeonsAndDungeons
{
    public class Item
    {
        public Dictionary<string, object> Attributes { get; set; }
        public Sprite Sprite { get; set; }

        public Item(Sprite sprite, Dictionary<string, object> attributes = default)
        {
            Sprite = sprite;
            Attributes = attributes;
        }
    }
}
