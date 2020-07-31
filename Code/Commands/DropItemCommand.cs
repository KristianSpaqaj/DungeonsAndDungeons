using DungeonsAndDungeons.Entities;

namespace DungeonsAndDungeons.Commands
{
    class DropItemCommand : Command
    {
        private Item Item { get; }

        public DropItemCommand(Entity entity, Level level, GameContext ctx, Item item) : base(entity, level, ctx)
        {
            Item = item;
        }

        public override void Execute()
        {
            Item.Position = Creator.Position;
            Creator.Inventory.Remove(Item);
            Level.Items.Add(Item);
        }
    }
}
