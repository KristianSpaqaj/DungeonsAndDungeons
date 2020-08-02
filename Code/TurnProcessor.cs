using DungeonsAndDungeons.Commands;
using DungeonsAndDungeons.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace DungeonsAndDungeons
{
    public class TurnProcessor
    {
        private enum States { PLAYER_TURN, OTHER_TURN }
        private States State { get; set; }
        private Command TurnCommand { get; set; }
        private double TimeOutPeriod { get; set; }
        private double TimeSinceLastTurn { get; set; }
        private int CurrentIndex { get; set; }
        private List<Entity> Entities { get; set; }
        private Entity Current { get; set; }

        public TurnProcessor(double timeOutPeriod = 0.25)
        {
            State = States.PLAYER_TURN;
            TimeOutPeriod = timeOutPeriod;
            TimeSinceLastTurn = 0;
            Entities = new List<Entity>();
        }

        /// <summary>
        /// Gets and runs actions from player and entities
        /// </summary>
        /// <remarks>Waits a predetermined amount betweeen each command execution</remarks>
        /// <param name="currentLevel"></param>
        /// <param name="ctx"></param>
        public void RunCurrentTurn(Level currentLevel, GameContext ctx)
        {
            Entities.Clear();
            Entities.Add(currentLevel.Player);
            Entities.AddRange(currentLevel.Entities);

            Current = Entities[CurrentIndex];

            if (ctx.GameTime.TotalGameTime.TotalSeconds - TimeSinceLastTurn > TimeOutPeriod)
            {
                TurnCommand = Current.GetAction(currentLevel, ctx);
                if (TurnCommand is FinishTurnCommand || Current.ActionPoints.Remaining <= Current.ActionPoints.Minimum)
                {
                    Current.ActionPoints.Remaining = Current.ActionPoints.Maximum;
                    if (CurrentIndex < Entities.Count - 1)
                    {
                        CurrentIndex++;
                    }
                    else
                    {
                        CurrentIndex = 0;
                    }
                }

                else if (!(TurnCommand is EmptyCommand) && TurnCommand.ActionCost <= Current.ActionPoints.Remaining)
                {
                    TurnCommand.Execute();
                    Current.Update();
                    Current.ActionPoints.Remaining -= TurnCommand.ActionCost;
                    TimeSinceLastTurn = ctx.GameTime.TotalGameTime.TotalSeconds;
                }
            }

        }

        private bool RunEntityTurn(Entity entity, Level level, GameContext ctx)
        {
            TurnCommand = entity.GetAction(level, ctx);
            if (TurnCommand != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
