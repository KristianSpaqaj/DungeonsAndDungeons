using System;
using System.Linq;

namespace DungeonsAndDungeons
{
    static class SeedGenerator
    {
        private static readonly Random random = new Random();
        public static string Generate(int length)
        {
            const string chars = "123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
