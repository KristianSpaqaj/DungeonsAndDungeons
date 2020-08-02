using DungeonsAndDungeons.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace DungeonsAndDungeons.GUI
{
    public class GUIRenderer
    {
        private SpriteFont Font { get; set; }
        private GraphicsDeviceManager Graphics { get; }
        private Rectangle InventoryRectangle { get; }
        private Rectangle HealthBoxRectangle { get; }
        private Rectangle HealthBarRectangle { get; }
        private GameWindow Window { get; }

        public GUIRenderer(GraphicsDeviceManager graphics, GameWindow window, SpriteFont font)
        {
            Font = font;
            Graphics = graphics;
            InventoryRectangle = new Rectangle(0, window.ClientBounds.Height - 200, window.ClientBounds.Width, 200);
            HealthBoxRectangle = new Rectangle(0, InventoryRectangle.Y - 50, 200, 50);
            HealthBarRectangle = new Rectangle(HealthBoxRectangle.X + 5, HealthBoxRectangle.Y + 5, HealthBoxRectangle.Width - 10, HealthBoxRectangle.Height - 10);
            Window = window;
        }

        public void Render(SpriteBatch batch, Level level)
        {
            batch.Begin();
            RenderDebugInfo(batch);
            RenderPlayerInfo(batch, level.Player);
            batch.End();
        }

        private void RenderPlayerInfo(SpriteBatch batch, Player player)
        {
            RenderInventory(batch, player.Inventory);
            RenderHealthBar(batch, player.Health);
            RenderActionPoints(batch, player.ActionPoints);
        }

        private void RenderActionPoints(SpriteBatch batch, ActionPoints actionPoints)
        {
            batch.DrawString(Font, $"Total Action Points: {actionPoints.Maximum}", new Vector2(Window.ClientBounds.Width * 0.9f, Window.ClientBounds.Height * 0.2f), Color.White);
            batch.DrawString(Font, $"Remaining: {actionPoints.Remaining}", new Vector2(Window.ClientBounds.Width * 0.9f, Window.ClientBounds.Height * 0.25f), Color.White);
        }

        private void RenderHealthBar(SpriteBatch batch, Health health)
        {
            batch.Draw(MakeTexture(Color.Magenta), HealthBoxRectangle, Color.White);
            batch.Draw(MakeTexture(Color.White), HealthBarRectangle, Color.White);

            int step = (int)Math.Round((double)HealthBarRectangle.Width / health.Maximum);

            for (int i = 0; i < health.Remaining; i++)
            {
                batch.Draw(MakeTexture(Color.Red), new Rectangle(i * step, HealthBarRectangle.Y, step, HealthBarRectangle.Height), Color.White);
            }
        }

        private void RenderInventory(SpriteBatch batch, Inventory inventory)
        {
            batch.Draw(MakeTexture(Color.DarkMagenta), InventoryRectangle, Color.White);

            if (inventory.Size > 0)
            {
                int scaleFactor = 2; // TODO find way of scaling (possibly using content rectangle)

                int y = InventoryRectangle.Y;

                for (int i = 0; i < inventory.Size; i++)
                {
                    if (inventory.SelectedSlot == i)
                    {
                        batch.Draw(MakeTexture(Color.White), new Rectangle(128 * i, y, 128, 128), Color.White);
                    }

                    if (!inventory.IsSlotEmpty(i))
                    {
                        batch.Draw(inventory[i].Sprite, new Rectangle(128 * i, y, 128, 128), Color.White);
                    }
                }
            }

        }
        private void RenderDebugInfo(SpriteBatch batch)
        {
            batch.DrawString(Font, string.Join(" , ", InputState.Actions), new Vector2(100, 100), Color.LimeGreen,
                            0.0f, new Vector2(0, 0), 2, SpriteEffects.None, 0);
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
