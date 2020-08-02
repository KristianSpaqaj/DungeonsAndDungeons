using DungeonsAndDungeons.Commands;
using DungeonsAndDungeons.Entities;
using System;

namespace DungeonsAndDungeons
{
    public class TurnProcessor
    {
        private enum States { PLAYER_TURN, OTHER_TURN }
        private States State { get; set; }
        private Command TurnCommand { get; set; }
        private double TimeOutPeriod { get; set; }
        private double TimeSinceLastTurn { get; set; }

        public TurnProcessor(double timeOutPeriod=0.25)
        {
            State = States.PLAYER_TURN;
            TimeOutPeriod = timeOutPeriod;
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

                    if (RunEntityTurn(currentLevel.Player, currentLevel, ctx))
                    {
                        TimeSinceLastTurn = ctx.GameTime.TotalGameTime.TotalSeconds;
                        State = States.OTHER_TURN;
                    }
                }
                else
                {
                    currentLevel.Entities.ForEach(e => RunEntityTurn(e, currentLevel, ctx));
                    State = States.PLAYER_TURN;
                }
            }
        }

        private bool RunEntityTurn(Entity entity, Level level, GameContext ctx)
        {
            TurnCommand = entity.GetAction(level, ctx);
            if (TurnCommand != null)
            {
                TurnCommand.Execute();
                entity.Update();
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
