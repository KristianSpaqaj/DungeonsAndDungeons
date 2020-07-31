using System.Collections.Generic;

namespace DungeonsAndDungeons
{
    /// <summary>
    /// Holds the translated actions of a single frame
    /// </summary>
    public static class InputState
    {
        public static List<string> Actions { get; set; }

        static InputState()
        {
            Actions = new List<string>();
        }

        /// <summary>
        /// Checks if a certain action happened during frame
        /// </summary>
        /// <param name="action"></param>
        /// <returns>True if action happened this frame, false otherwise</returns>
        public static bool HasAction(string action)
        {
            return Actions.Contains(action);
        }

    }
}
