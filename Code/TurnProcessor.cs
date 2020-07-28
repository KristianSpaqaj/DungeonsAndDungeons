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
                    if (RunPlayerTurn(currentLevel, ctx)) // Split into checking and running methods
                    {
                        State = States.OTHERTURN;
                        TimeSinceLastTurn = ctx.GameTime.TotalGameTime.TotalSeconds;
                    }
                }
                else
                {
                    RunEntitiesTurn(currentLevel, ctx);
                    State = States.WAIT_FOR_INPUT;
                    TimeSinceLastTurn = ctx.GameTime.TotalGameTime.TotalSeconds;
                }
            }

        }

        private bool RunPlayerTurn(Level level, GameContext ctx)
        {
            TurnCommand = level.Player.GetAction(level, ctx); // player should perhaps have a set amount of comands they can build per turn
            if (TurnCommand == null) // TODO find better way of doing this
            {
                return false;
            }
            else
            {
                TurnCommand.Execute();
                return true;
            }
        }

        private void RunEntitiesTurn(Level level, GameContext ctx)
        {
            foreach (Entity entity in level.Entities)
            {
                {
                    TurnCommand = entity.GetAction(level, ctx);
                    TurnCommand.Execute();
                  
                }
            }
        }
    }
}
