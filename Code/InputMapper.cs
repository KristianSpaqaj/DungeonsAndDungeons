using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace DungeonsAndDungeons
{
    internal class InputMapper
    {
        internal List<string> Translate(Keys[] keys, Dictionary<string, string> bindings)
        {
            List<string> res = new List<string>();
            foreach (Keys k in keys)
            {
                if (bindings.ContainsKey(k.ToString()))
                {
                    res.Add(bindings[k.ToString()]);
                }
                else
                {
                    res.Add("UNDEFINED");
                }
            }

            return res;
        }
    }
}