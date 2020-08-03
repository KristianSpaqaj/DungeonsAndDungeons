using DungeonsAndDungeons.Commands;
using DungeonsAndDungeons.Entities;

namespace DungeonsAndDungeons.Commands
{
    public class RotateRightCommand : RotateCommand
    {
        public override int Direction => -1;
        public RotateRightCommand(Entity entity, Level level, GameContext ctx) : base(entity, level, ctx)
        {
        }
    }
}