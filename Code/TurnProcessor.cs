using DungeonsAndDungeons.Commands;
using DungeonsAndDungeons.Entities;
using System.Collections.Generic;

namespace DungeonsAndDungeons
{
    public class TurnProcessor
    {
        private Command TurnCommand { get; set; }
        private double TimeOutPeriod { get; set; }
        private double TimeSinceLastTurn { get; set; }
        private int CurrentIndex { get; set; }
        private List<Entity> Entities { get; set; }
        private Entity Current { get; set; }

        public TurnProcessor(double timeOutPeriod = 0.25)
        {
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
            InitializeTurn(currentLevel);

            if (ctx.GameTime.TotalGameTime.TotalSeconds - TimeSinceLastTurn > TimeOutPeriod)
            {
                TurnCommand = Current.GetAction(currentLevel, ctx);
                if (TurnOver())
                {
                    GoToNextTurn();
                }

                else if (ValidCommand())
                {
                    RunAction();
                    TimeSinceLastTurn = ctx.GameTime.TotalGameTime.TotalSeconds;
                }
            }

        }

        private void InitializeTurn(Level level)
        {
            Entities.Clear();
            Entities.Add(level.Player);
            Entities.AddRange(level.Entities);

            Current = Entities[CurrentIndex];
        }

        private bool TurnOver()
        {
            return TurnCommand is FinishTurnCommand || Current.ActionPoints.IsMinimum();
        }

        private bool ValidCommand()
        {
            return !(TurnCommand is EmptyCommand) && TurnCommand.ActionCost <= Current.ActionPoints;
        }

        private void RunAction()
        {
            TurnCommand.Execute();
            Current.Update();
            Current.ActionPoints.Remaining -= TurnCommand.ActionCost;
        }

        private void GoToNextTurn()
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
