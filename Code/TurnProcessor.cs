using DungeonsAndDungeons.Commands;
using DungeonsAndDungeons.Entities;
using Microsoft.Xna.Framework;
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
        public TurnProcessor(double timeOutPeriod = 0.25)
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
            TurnCommand = null;
            List<Entity> entities = new List<Entity>() { };
            entities.Add(currentLevel.Player);
            entities.AddRange(currentLevel.Entities);

            if (ctx.GameTime.TotalGameTime.TotalSeconds - TimeSinceLastTurn > TimeOutPeriod)
            {
                TurnCommand = entities[CurrentIndex].GetAction(currentLevel, ctx);
                if (TurnCommand is FinishTurnCommand || entities[CurrentIndex].ActionPoints.Remaining <= entities[CurrentIndex].ActionPoints.Minimum)
                {
                    entities[CurrentIndex].ActionPoints.Remaining = entities[CurrentIndex].ActionPoints.Maximum;
                    if (CurrentIndex < entities.Count - 1)
                    {
                        CurrentIndex++;
                    }
                    else
                    {
                        CurrentIndex = 0;
                    }
                }

                else if (TurnCommand != null)
                {
                    TurnCommand.Execute();
                    entities[CurrentIndex].Update();
                    entities[CurrentIndex].ActionPoints.Remaining -= TurnCommand.ActionCost;
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
