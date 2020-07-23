using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Input;

namespace DungeonsAndDungeons
{
    internal class InputMapper
    {
        internal List<string> Translate(Keys[] keys)
        {
            return keys.Select((k) => k.ToString()).ToList();
        }
    }
}