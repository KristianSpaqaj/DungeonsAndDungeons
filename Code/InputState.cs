using System.Collections.Generic;

namespace DungeonsAndDungeons.Code
{
    public static class InputState
    {
        public static List<string> Actions { get; set; }

        static InputState()
        {
            Actions = new List<string>();
        }


        public static bool HasAction(string action)
        {
            return Actions.Contains(action);
        }

    }
}
