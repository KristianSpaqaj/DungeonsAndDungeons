using DungeonsAndDungeons.Extensions;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace DungeonsAndDungeons
{
    public class InputMapper
    {
        Dictionary<string, string> Bindings { get; }

        public InputMapper(Dictionary<string, string> bindings)
        {
            Bindings = bindings;
        }

        /// <summary>
        /// Translates the raw keyboard buttone to actions by looking them up in it's keybindings
        /// </summary>
        /// <param name="keys"></param>
        /// <returns>A list of translated actions, with "UNDEFINED" represnting a keystroke not in the keybindings</returns>
        public List<string> Translate(Keys[] keys)
        {
            return keys.Select((k) => Bindings.GetOrDefault(k.ToString(), "UNDEFINED")).ToList(); //TODO should maybe throw exception instead
        }
    }
}