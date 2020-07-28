using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

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