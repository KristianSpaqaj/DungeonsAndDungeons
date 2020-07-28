using DungeonsAndDungeons.Commands;

namespace DungeonsAndDungeons
{
    public class TurnProcessor
    {
        private enum States { PLAYER_TURN, OTHER_TURN }
        private States State { get; set; }
        private Command TurnCommand { get; set; }
        private double TimeOutPeriod { get; set; }
        private double TimeSinceLastTurn { get; set; }

        public TurnProcessor()
        {
            State = States.PLAYER_TURN;
            TimeOutPeriod = 0.5;
            TimeSinceLastTurn = 0;
        }

        public void RunCurrentTurn(Level currentLevel, GameContext ctx)
        {
            if (ctx.GameTime.TotalGameTime.TotalSeconds - TimeSinceLastTurn > TimeOutPeriod)
            {
                if (State == States.PLAYER_TURN)
                {
                    if (RunPlayerTurn(currentLevel, ctx)) // Split into checking and running methods
                    {
                        State = States.OTHER_TURN;
                        TimeSinceLastTurn = ctx.GameTime.TotalGameTime.TotalSeconds;
                    }
                }
                else
                {
                    RunEntitiesTurn(currentLevel, ctx);
                    State = States.PLAYER_TURN;
                    TimeSinceLastTurn = ctx.GameTime.TotalGameTime.TotalSeconds;
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
