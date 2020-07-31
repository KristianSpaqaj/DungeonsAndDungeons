using DungeonsAndDungeons.Commands;
using DungeonsAndDungeons.Entities;

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
            TimeOutPeriod = 0.25;
            TimeSinceLastTurn = 0;
        }

        /// <summary>
        /// Gets and runs actions from player and entities
        /// </summary>
        /// <remarks>Waits a predetermined amount betweeen each command execution</remarks>
        /// <param name="currentLevel"></param>
        /// <param name="ctx"></param>
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
        }

        /// <summary>
        /// Gets action from player and executes it
        /// </summary>
        /// <param name="level"></param>
        /// <param name="ctx"></param>
        /// <returns>True if player has returned a non-null command, false otherwise</returns>
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

        /// <summary>
        /// Runs through entities in <paramref name="level"/>, getting and executing actione from each
        /// </summary>
        /// <param name="level"></param>
        /// <param name="ctx"></param>
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
