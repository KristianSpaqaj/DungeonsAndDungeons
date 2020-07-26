using Microsoft.Xna.Framework;

namespace DungeonsAndDungeons
{
    public class GameContext
    {
        public GameTime GameTime { get; set; }

        public GameContext(GameTime gameTime)
        {
            GameTime = gameTime;
        }
    }
}