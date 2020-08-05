using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace DungeonsAndDungeons
{
    public static class MouseInfo
    {
        public static int X => CurrentState.X;
        public static int Y => CurrentState.Y;
        public static bool LeftButtonPressed => CurrentState.LeftButton == ButtonState.Pressed;
        public static bool RightButtonPressed => CurrentState.RightButton == ButtonState.Pressed;
        public static bool LeftButtonClicked => LeftButtonPressed && OldState.LeftButton == ButtonState.Released;
        public static bool RightButtonClicked => RightButtonPressed && OldState.RightButton == ButtonState.Released;
        public static bool ScrollWheelUp => CurrentState.ScrollWheelValue - OldState.ScrollWheelValue > 0;
        public static bool ScrollWheelDown => CurrentState.ScrollWheelValue - OldState.ScrollWheelValue < 0;
        private static MouseState OldState { get; set; }
        private static MouseState CurrentState { get; set; }

        static MouseInfo()
        {
            CurrentState = Mouse.GetState();
            OldState = CurrentState;
        }

        public static void Update(MouseState newState)
        {
            OldState = CurrentState;
            CurrentState = newState;
        }

        public static List<string> GetPressed()
        {
            List<string> info = new List<string>();

            if (LeftButtonPressed)
            {
                info.Add(nameof(LeftButtonPressed));
            }
            else if (LeftButtonClicked)
            {
                info.Add(nameof(LeftButtonClicked));
            }
            else if (RightButtonPressed)
            {
                info.Add(nameof(RightButtonPressed));
            }
            else if (RightButtonClicked)
            {
                info.Add(nameof(RightButtonClicked));
            }
            if (ScrollWheelUp)
            {
                info.Add(nameof(ScrollWheelUp));
            }
            else if (ScrollWheelDown)
            {
                info.Add(nameof(ScrollWheelDown));
            }

            return info;

        }

    }
}
