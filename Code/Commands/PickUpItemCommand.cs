using DungeonsAndDungeons.Commands;
using DungeonsAndDungeons.Entities;
using System.Collections.Generic;

namespace DungeonsAndDungeons.Commands
{
    internal class PickUpItemCommand : Command
    {
        private Entity Entity;
        private GameContext Ctx;
        private Item Item;

        public PickUpItemCommand(Entity entity, Level level, GameContext ctx, Item item) :  base(entity,level,ctx)
        {
            Entity = entity;
            Level = level;
            Ctx = ctx;
            Item = item;
        }

        public override void Execute()
        {
            Entity.Inventory.AddItem(Item);
            Level.Items.Remove(Item);
        }
    }
}