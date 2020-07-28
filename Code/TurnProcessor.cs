using DungeonsAndDungeons.Commands;

namespace DungeonsAndDungeons
{
    public class TurnProcessor
    {
        private enum States { WAIT_FOR_INPUT, OTHERTURN }
        private States State { get; set; }
        private Command TurnCommand { get; set; }
        private double TimeOutPeriod { get; set; }
        private double TimeSinceLastTurn { get; set; }


        public TurnProcessor()
        {
            State = States.WAIT_FOR_INPUT;
            TimeOutPeriod = 0.5;
            TimeSinceLastTurn = 0;
        }

        public void RunCurrentTurn(Level currentLevel, GameContext ctx)
        {
            if (ctx.GameTime.TotalGameTime.TotalSeconds - TimeSinceLastTurn > TimeOutPeriod)
            {
                if (State == States.WAIT_FOR_INPUT)
                {
                    TurnCommand = currentLevel.Player.GetAction(currentLevel, ctx); // player should perhaps have a set amount of comands they can build per turn
                    if (TurnCommand != null) // TODO find better way of doing this
                    {
                        TurnCommand.Execute();
                        State = States.OTHERTURN;
                        TimeSinceLastTurn = ctx.GameTime.TotalGameTime.TotalSeconds;
                    }
                }
                else
                {
                    foreach (Entity entity in currentLevel.Entities)
                    {
                        {
                            TurnCommand = entity.GetAction(currentLevel, ctx);
                            TurnCommand.Execute();
                            State = States.WAIT_FOR_INPUT;
                            TimeSinceLastTurn = ctx.GameTime.TotalGameTime.TotalSeconds;
                        }
                    }
                }
            }

        }
    }
}
