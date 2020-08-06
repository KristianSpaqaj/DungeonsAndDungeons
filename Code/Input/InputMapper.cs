using System.Collections.Generic;

namespace DungeonsAndDungeons.Input
{
    public class InputMapper
    {
        Dictionary<string, InputAction> Bindings { get; }

        public InputMapper(Dictionary<string, InputAction> bindings)
        {
            Bindings = bindings;
        }

        public List<InputAction> Translate(List<string> inputs)
        {
            List<InputAction> output = new List<InputAction>();
            foreach (string input in inputs)
            {
                if (Bindings.ContainsKey(input))
                {
                    output.Add(Bindings[input]);
                }
            }

            return output;
        }
    }
}