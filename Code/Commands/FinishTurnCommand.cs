﻿using DungeonsAndDungeons.Entities;

namespace DungeonsAndDungeons.Commands
{
    class FinishTurnCommand : Command
    {
        public override int ActionCost => 0;
        public FinishTurnCommand(Entity entity, Level level, GameContext ctx) : base(entity, level, ctx) { }
        public override void Execute()
        {
            Creator.ActionPoints.Remaining = Creator.ActionPoints.Minimum;
        }
    }
}
