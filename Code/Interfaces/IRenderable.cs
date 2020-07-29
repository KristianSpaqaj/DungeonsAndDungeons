using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDungeons.Interfaces
{
    public interface IRenderable
    {
        Sprite Sprite { get; }
        Vector2 Position { get; }
    }
}
