using Microsoft.Xna.Framework;

namespace DungeonsAndDungeons
{
    public abstract class Behaviour
    {
        public abstract void Run(Entity caller, Level level, GameTime gameTime);
    } 
}