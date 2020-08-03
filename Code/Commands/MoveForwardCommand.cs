using DungeonsAndDungeons.Commands;
using DungeonsAndDungeons.Entities;

namespace DungeonsAndDungeons.Commands
{
    public class MoveForwardCommand : MoveCommand
    {
        protected override int Direction => 1;
        public MoveForwardCommand(Entity entity, Level level, GameContext ctx) : base(entity, level, ctx) { }
    }
}