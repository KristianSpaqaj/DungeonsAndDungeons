using Microsoft.Xna.Framework;

namespace DungeonsAndDungeons
{
    public abstract class Behaviour
    {
        public abstract void Run(ref Entity caller, ref Level level, GameTime gameTime);
    }
}