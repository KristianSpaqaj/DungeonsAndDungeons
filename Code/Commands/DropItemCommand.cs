using DungeonsAndDungeons.Entities;

namespace DungeonsAndDungeons.Commands
{
    class DropItemCommand : Command
    {
        public override int ActionCost => 1;

        public DropItemCommand(Entity entity, Level level, GameContext ctx) : base(entity, level, ctx)
        {
        }

        public override void Execute()
        {
            Item item = Creator.Inventory.Selected;
            Creator.Inventory.Remove(Creator.Inventory.SelectedSlot);
            Level.Items.Add(item);
        }

        public override bool CanRun()
        {
            return Creator.Inventory.Selected != null;
        }
    }
}
