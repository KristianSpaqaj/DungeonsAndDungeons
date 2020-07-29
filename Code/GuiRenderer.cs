using DungeonsAndDungeons.Entities;
using Microsoft.Xna.Framework;

namespace DungeonsAndDungeons
{
    public class GuiRenderer
    {
        private readonly Color[] _buffer;

        public GuiRenderer(int width, int height)
        {
            _buffer = new Color[width * height];
        }
        public Color[] Render(Player player)
        {
            return _buffer;
        }
    }
}
