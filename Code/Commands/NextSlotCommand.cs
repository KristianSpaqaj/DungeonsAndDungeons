using DungeonsAndDungeons.Entities;

namespace DungeonsAndDungeons.Commands
{
    public class NextSlotCommand : ChangeInventorySlotCommand
    {
        protected override int Slot => 1;
        public NextSlotCommand(Entity entity, Level level, GameContext ctx) : base(entity, level, ctx) { }

        public override bool CanRun()
        {
            return Creator.Inventory.SelectedSlot + Slot < (Creator.Inventory.Size - 1);
        }
    }
}
