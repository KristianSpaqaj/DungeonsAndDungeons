using DungeonsAndDungeons.Entities;

namespace DungeonsAndDungeons.Commands
{
    internal class PickUpItemCommand : Command
    {
        private readonly Item Item;

        public PickUpItemCommand(Entity entity, Level level, GameContext ctx, Item item) : base(entity, level, ctx)
        {
            Item = item;
        }

        public override void Execute()
        {
            Creator.Inventory.Add(Item);
            Level.Items.Remove(Item);
        }
    }
}