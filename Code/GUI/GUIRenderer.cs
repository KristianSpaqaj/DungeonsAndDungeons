using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DungeonsAndDungeons.GUI
{
    public class GUIRenderer
    {
        private SpriteFont Font { get; set; }
        private GraphicsDeviceManager Graphics { get; }
        private Rectangle InventoryRectangle { get; }

        public GUIRenderer(GraphicsDeviceManager graphics, GameWindow window, SpriteFont font)
        {
            Font = font;
            Graphics = graphics;
            InventoryRectangle = new Rectangle(0, window.ClientBounds.Height - 200, window.ClientBounds.Width, 200);
        }

        public void Render(SpriteBatch batch, Level level)
        {
            RenderDebugInfo(batch);
            RenderInventory(batch, level.Player.Inventory);
        }

        private void RenderDebugInfo(SpriteBatch batch)
        {
            batch.DrawString(Font, string.Join(" , ", InputState.Actions), new Vector2(100, 100), Color.LimeGreen,
                            0.0f, new Vector2(0, 0), 2, SpriteEffects.None, 0);
        }

        private void RenderInventory(SpriteBatch batch, Inventory inventory)
        {
            batch.Draw(MakeTexture(Color.DarkMagenta), InventoryRectangle, Color.White);

            if(inventory.Count > 0)
            {
                int scaleFactor = 2; // TODO find way of scaling (possibly using content rectangle)

                int itemWidth = inventory[0].Sprite.Width * scaleFactor;
                int itemHeight = inventory[0].Sprite.Height * scaleFactor;

                int y = InventoryRectangle.Y;

                for (int i = 0; i < inventory.Count; i++)
                {
                    batch.Draw(inventory[i].Sprite, new Rectangle(itemWidth * i, y, itemWidth, itemHeight), Color.White);
                }
            }

        }

        private Texture2D MakeTexture(Color color)
        {
            Texture2D texture = new Texture2D(Graphics.GraphicsDevice, 1, 1);
            Color[] textureData = new Color[1] { color };
            texture.SetData<Color>(textureData);
            return texture;
        }
    }
}
