using System.Collections.Generic;

namespace DungeonsAndDungeons
{
    public class Entity
    {
        public int Id { get; }
        public Dictionary<string, object> Attributes { get; set; }
        public Behaviour Behaviour { get; set; }

        public Entity()
        {
            //generate ID
        }

        public bool HasAttribute(string attribute)
        {
            return Attributes.ContainsKey(attribute);
        }
    }
}
