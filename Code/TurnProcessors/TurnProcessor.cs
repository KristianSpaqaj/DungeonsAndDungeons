using DungeonsAndDungeons.Commands;
using DungeonsAndDungeons.Entities;
using DungeonsAndDungeons.Interfaces;
using System;
using System.Collections.Generic;

namespace DungeonsAndDungeons.TurnProcessors
{
    public abstract class TurnProcessor
    {
        protected double TimeOutPeriod { get; set; }
        protected double TimeSinceLastTurn { get; set; }
        private int CurrentIndex { get; set; }
        protected List<Entity> Entities { get; set; }
        protected Entity Current { get; set; }
        private ICommand TurnCommand { get; set; }
        protected List<Type> InvalidCommands { get; }

        public TurnProcessor(double timeOutPeriod = 0.25)
        {
            TimeOutPeriod = timeOutPeriod;
            TimeSinceLastTurn = 0;
            Entities = new List<Entity>();
            InvalidCommands = new List<Type>() { typeof(EmptyCommand) };
        }

        public void RunCurrentTurn(Level currentLevel, GameContext ctx)
        {
            if (ctx.GameTime.ElapsedGameTime.TotalSeconds - TimeSinceLastTurn > TimeOutPeriod)
            {
                InitializeTurn(currentLevel);
                TurnCommand = Current.GetAction(currentLevel, ctx);
                if (TurnOver())
                {
                    GoToNextTurn();
                }

                else if (ValidCommand())
                {
                    RunTurnCommand();
                }
            }
        }
        protected virtual void InitializeTurn(Level level)
        {
            Entities.Clear();
            Entities.Add(level.Player);
            Entities.AddRange(level.Entities);

            Current = Entities[CurrentIndex];
        }

        protected virtual bool TurnOver()
        {
            return TurnCommand is FinishTurnCommand || Current.ActionPoints.IsMinimum();
        }

        protected virtual bool ValidCommand()
        {
            return !InvalidCommands.Contains(TurnCommand.GetType());
        }

        protected void RunTurnCommand()
        {
            TurnCommand.Execute();
            Current.Update();
            Current.ActionPoints.Remaining -= TurnCommand.ActionCost;
        }

        protected void GoToNextTurn()
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
    }
}
