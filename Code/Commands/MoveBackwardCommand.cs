using DungeonsAndDungeons.Entities;

namespace DungeonsAndDungeons.Commands
{
    internal class MoveBackwardCommand : MoveCommand
    {
        protected override int Direction => -1;
        public MoveBackwardCommand(Entity entity, Level level, GameContext ctx) : base(entity, level, ctx) { }
    }
}