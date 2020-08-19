using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDungeons
{
    public static class ContentContainer
    {
        public static ContentManager Manager { get; private set; }
        public static void Initialize(ContentManager mg)
        {
            Manager = mg;
        }
    }
}
