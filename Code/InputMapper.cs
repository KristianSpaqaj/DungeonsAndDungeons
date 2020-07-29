using DungeonsAndDungeons.Extensions;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace DungeonsAndDungeons
{
    public class InputMapper
    {
        Dictionary<string, string> Bindings { get; set; }

        public InputMapper(Dictionary<string, string> bindings)
        {
            Bindings = bindings;
        }

        public List<string> Translate(Keys[] keys)
        {
            return keys.Select((k) => Bindings.GetOrDefault(k.ToString(), "UNDEFINED")).ToList(); //TODO should maybe throw exception instead
        }
    }
}