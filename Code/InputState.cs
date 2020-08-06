using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace DungeonsAndDungeons
{
    /// <summary>
    /// Holds the translated actions of a single frame
    /// </summary>
    public static class InputState
    {
        public static List<InputAction> Actions { get; set; }
        private static Dictionary<string, double> TimeOutTable { get; set; }

        public static void Initalize()
        {
            Actions = new List<InputAction>();
            TimeOutTable = new Dictionary<string, double>();
        }



        /// <summary>
        /// Checks if a certain action happened during frame
        /// </summary>
        /// <param name="action"></param>
        /// <returns>True if action happened this frame, false otherwise</returns>
        public static bool HasAction(string action)
        {
            InputAction inputAction = Actions.FirstOrDefault(a => a.Action == action);
            if (inputAction == null)
            {
                return false;
            }

            if (!TimeOutTable.ContainsKey(inputAction.Action))
            {
                TimeOutTable[inputAction.Action] = TimeTracker.GameTime.TotalGameTime.TotalSeconds;
                return true;
            }
            else
            {
                if (TimeTracker.GameTime.TotalGameTime.TotalSeconds - TimeOutTable[inputAction.Action] > inputAction.TimeOut)
                {
                    TimeOutTable[inputAction.Action] = TimeTracker.GameTime.TotalGameTime.TotalSeconds;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
