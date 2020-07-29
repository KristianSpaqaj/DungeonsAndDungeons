using System.Collections.Generic;

namespace DungeonsAndDungeons.Extensions
{
    public static class DictionaryExtension
    {
        public static T2 GetOrDefault<T, T2>(this Dictionary<T, T2> dict, T key, T2 defaultValue)
        {
            if (dict.ContainsKey(key))
            {
                return dict[key];
            }
            else
            {
                return defaultValue;
            }
        }
    }
}
