using System.Collections.Generic;

namespace DungeonsAndDungeons
{
    public class Item
    {
        public Dictionary<string, object> Attributes { get; set; }
        public Sprite Sprite { get; set; }

        public Item(Dictionary<string, object> attributes, Sprite sprite)
        {
            Attributes = attributes;
            Sprite = sprite;
        }
    }
}
