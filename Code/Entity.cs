using System;
using System.CodeDom;
using System.Collections.Generic;
using DungeonsAndDungeons.Attributes;
using Attribute = DungeonsAndDungeons.Attributes.Attribute;

namespace DungeonsAndDungeons
{
    public class Entity
    {
        public int Id { get; }
        public Dictionary<Type, Attribute> Attributes { get; set; } = new Dictionary<Type, Attribute>();
        public Behaviour Behaviour { get; set; }

        public Entity(List<Attribute> attributes)
        {
            foreach(Attribute attr in attributes)
            {
                Attributes[attr.GetType()] = attr;
            }
            //generate ID
        }

        public Entity(List<Attribute> attributes, Behaviour behaviour) : this(attributes)
        {
            Behaviour = behaviour;
        }

        public bool HasAttribute<T>()
        {
            return Attributes.ContainsKey(typeof(T));
        }

        public T GetAttribute<T>()
        {
            return (T)Convert.ChangeType(Attributes[typeof(T)], typeof(T));
        }

        public void SetAttribute<T>(Attribute attribute)
        {
            Attributes[typeof(T)] = attribute;
        }
    }
}
