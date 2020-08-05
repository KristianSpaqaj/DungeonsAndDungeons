using DungeonsAndDungeons.Entities;

namespace DungeonsAndDungeons.Commands
{
    public abstract class ChangeInventorySlotCommand : Command
    {
        protected abstract int Slot { get; }
        public override int ActionCost => 0;
        public override bool TimesOut => false;

        public ChangeInventorySlotCommand(Entity entity, Level level, GameContext ctx) : base(entity, level, ctx) { }

        public override void Execute()
        {
            Creator.Inventory.SelectedSlot += Slot;
        }
    }
}
