using DungeonsAndDungeons.Entities;
using System;

namespace DungeonsAndDungeons.Commands
{
    class FinishTurnCommand : Command
    {

        public FinishTurnCommand(Entity entity, Level level, GameContext ctx) : base(entity, level, ctx) { }
        public override void Execute()
        {
            Creator.ActionPoints.Remaining = Creator.ActionPoints.Minimum;
        }
    }
}
