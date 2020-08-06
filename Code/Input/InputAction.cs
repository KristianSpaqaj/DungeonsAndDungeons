namespace DungeonsAndDungeons.Input
{
    public class InputAction
    {
        public string Action { get; }
        public double TimeOut { get; }
        public bool IsRepeatable { get; }

        public InputAction(string action, double timeOut, bool isRepeatable)
        {
            Action = action;
            TimeOut = timeOut;
            IsRepeatable = isRepeatable;
        }
    }
}
