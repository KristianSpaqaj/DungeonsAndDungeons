using DungeonsAndDungeons.Commands;
using DungeonsAndDungeons.Entities;

namespace DungeonsAndDungeons.Code.Commands
{
    public class SelectInventorySlotCommand : Command
    {
        public override int ActionCost => 0;
        public int Slot { get; set; 

        public SelectInventorySlotCommand(Entity entity, Level level, GameContext ctx) : base(entity, level, ctx)
        {
            Slot = 0;
        }

        public override void Execute()
        {
            Creator.Inventory.SelectedSlot = Slot;
        }
    }
}
