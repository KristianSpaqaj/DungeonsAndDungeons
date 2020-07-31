using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonsAndDungeons.GUI
{
    public class GUIRenderer
    {
        private SpriteFont Font { get; set; }
        
        public GUIRenderer(SpriteFont font)
        {
            Font = font;
        }

        public void Render(SpriteBatch batch, Level level)
        {
            batch.DrawString(Font, string.Join(" , ", InputState.Actions), new Vector2(100, 100), Color.LimeGreen);
            batch.DrawString(Font, string.Join(" , ", level.Player.Inventory), new Vector2(100, 200), Color.LimeGreen);
        }
    }
}
