using DungeonsAndDungeons.Entities;

namespace DungeonsAndDungeons.Commands
{
    public class RotateLeftCommand : RotateCommand
    {
        public override int Direction => 1;

        public RotateLeftCommand(Entity entity, Level level, GameContext ctx) : base(entity, level, ctx) { }
    }
}
