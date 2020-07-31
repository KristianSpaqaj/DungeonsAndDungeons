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
            RenderDebugInfo(batch);
            RenderInventory(batch, level.Player.Inventory);
        }

        private void RenderDebugInfo(SpriteBatch batch)
        {
            batch.DrawString(Font, string.Join(" , ", InputState.Actions), new Vector2(100, 100), Color.LimeGreen,
                            0.0f, new Vector2(0,0),2,SpriteEffects.None,0);
        }

        private void RenderInventory(SpriteBatch batch, Inventory inventory)
        {
            batch.DrawString(Font, string.Join(" , ", inventory), new Vector2(100, 200), Color.LimeGreen);
        }
    }
}
