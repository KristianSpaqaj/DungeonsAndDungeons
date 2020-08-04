using DungeonsAndDungeons.Entities;
using System.Collections.Generic;

namespace DungeonsAndDungeons.Commands
{
    internal class PickUpItemCommand : Command
    {

        public PickUpItemCommand(Entity entity, Level level, GameContext ctx) : base(entity, level, ctx){}

        public override void Execute()
        {
            List<Item> items = Level.ItemsAt((int)Creator.Position.X, (int)Creator.Position.Y);

            Creator.Inventory.Add(items[0]);
            Level.Items.Remove(items[0]);
        }

        public override bool CanRun()
        {
            return Level.ItemsAt((int)Creator.Position.X, (int)Creator.Position.Y).Count > 0;
        }
    }
}