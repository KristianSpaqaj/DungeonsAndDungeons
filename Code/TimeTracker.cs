using Microsoft.Xna.Framework;

namespace DungeonsAndDungeons
{
    public static class TimeTracker
    {
        public static GameTime GameTime { get; set; }
    
        public static void Initialize(GameTime gameTime)
        {
            GameTime = gameTime;
        }
    }
}
