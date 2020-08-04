using DungeonsAndDungeons.Commands;
using DungeonsAndDungeons.Entities;
using System;

namespace DungeonsAndDungeons
{
    public static class CommandFactory
    {
        public static Command Generate(Type type, Entity entity, Level level, GameContext ctx)
        {
            if (CompareTypes<FinishTurnCommand>(type))
            {
                return new FinishTurnCommand(entity, level, ctx);
            }
            else if (CompareTypes<MoveForwardCommand>(type))
            {
                return new MoveForwardCommand(entity, level, ctx);
            }
            else if (CompareTypes<RotateLeftCommand>(type))
            {
                return new RotateLeftCommand(entity, level, ctx);
            }
            else if (CompareTypes<RotateRightCommand>(type))
            {
                return new RotateRightCommand(entity, level, ctx);
            }
            else if (CompareTypes<MoveBackwardCommand>(type))
            {
                return new MoveBackwardCommand(entity, level, ctx);
            }
            else if (CompareTypes<PickUpItemCommand>(type))
            {
                return new PickUpItemCommand(entity, level, ctx);
            }
            else if (CompareTypes<DropItemCommand>(type))
            {
                return new DropItemCommand(entity, level, ctx);
            }
            else if (CompareTypes<NextSlotCommand>(type))
            {
                return new NextSlotCommand(entity, level, ctx);
            }
            else if (CompareTypes<PreviousSlotCommand>(type))
            {
                return new PreviousSlotCommand(entity, level, ctx);
            }
            else
            {
                throw new ArgumentException();
            }
        }

        private static bool CompareTypes<T>(Type t)
        {
            return typeof(T) == t;
        }
    }
}
