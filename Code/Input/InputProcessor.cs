using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace DungeonsAndDungeons.Input
{
    public class InputProcessor
    {
        private readonly InputMapper InputMapper;

        public InputProcessor(InputMapper inputMapper)
        {
            InputMapper = inputMapper;
        }

        public void ProcessInput()
        {
            List<string> pressed = Keyboard.GetState().GetPressedKeys().Select(k => k.ToString()).ToList();
            MouseInfo.Update(Mouse.GetState());
            pressed.AddRange(MouseInfo.GetPressed());
            InputState.Actions = InputMapper.Translate(pressed);
        }
    }
}
