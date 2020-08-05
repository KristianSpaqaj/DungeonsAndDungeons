using DungeonsAndDungeons.Extensions;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DungeonsAndDungeons
{
    public class InputMapper
    {
        Dictionary<string, string> Bindings { get; }
        private List<string> Output { get; set; }
        private Dictionary<string, string> MouseInfo { get; }

        public InputMapper(Dictionary<string, string> bindings)
        {
            Bindings = bindings;
            Output = new List<string>();
        }

        /// <summary>
        /// Translates the raw keyboard buttone to actions by looking them up in it's keybindings
        /// </summary>
        /// <param name="keys"></param>
        /// <returns>A list of translated actions, with "UNDEFINED" represnting a keystroke not in the keybindings</returns>
        public List<string> Translate(List<string> inputs)
        {
            return inputs.Select(k => Bindings.GetOrDefault(k.ToString(), "UNDEFINED")).ToList(); //TODO should) maybe throw exception instead
        }
    }
}