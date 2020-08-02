using DungeonsAndDungeons.Commands;
using DungeonsAndDungeons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDungeons.Code.Commands
{
    public class SelectInventorySlotCommand : Command
    {
        public int Slot { get; set; }

        public SelectInventorySlotCommand(Entity entity, Level level, GameContext ctx) : base(entity,level,ctx)
        {
            Slot = 0;
        }

        public override void Execute()
        {
            Creator.Inventory.SelectedSlot = Slot;
        }
    }
}
