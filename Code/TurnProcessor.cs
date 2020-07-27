using DungeonsAndDungeons.Commands;
using System;

namespace DungeonsAndDungeons
{
    public class TurnProcessor
    {
        private enum States { WAIT_FOR_INPUT, OTHERTURN }
        private States State { get; set; }
        private Command TurnCommand { get; set; }
        public TurnProcessor()
        {
            State = States.WAIT_FOR_INPUT;
        }

        public void RunCurrentTurn(Level currentLevel, GameContext ctx)
        {
            if(State == States.WAIT_FOR_INPUT)
            {
                TurnCommand = currentLevel.Player.GetAction(currentLevel, ctx);
                if(TurnCommand != null)
                {
                    TurnCommand.Execute();
                    State = States.OTHERTURN;
                }
            }else if(State == States.OTHERTURN)
            {
                foreach(Entity entity in currentLevel.Entities)
                {
                    TurnCommand = entity.GetAction(currentLevel,ctx);
                    TurnCommand.Execute();
                    State = States.WAIT_FOR_INPUT;
                }
            }
        }

    }
}
