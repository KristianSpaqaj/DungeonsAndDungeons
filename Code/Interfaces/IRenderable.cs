using Microsoft.Xna.Framework;

namespace DungeonsAndDungeons.Interfaces
{
    public interface IRenderable
    {
        Sprite Sprite { get; }
        Vector2 Position { get; }
    }
}
