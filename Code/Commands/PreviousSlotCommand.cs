using DungeonsAndDungeons.Entities;

namespace DungeonsAndDungeons.Commands
{
    public class PreviousSlotCommand : ChangeInventorySlotCommand
    {
        public override bool TimesOut => false;
        protected override int Slot => -1;
        public PreviousSlotCommand(Entity entity, Level level, GameContext ctx) : base(entity, level, ctx) { }

        public override bool CanRun()
        {
            return Creator.Inventory.SelectedSlot > 0;
        }
    }
}
