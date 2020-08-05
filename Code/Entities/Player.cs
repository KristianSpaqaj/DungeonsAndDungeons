using DungeonsAndDungeons.Commands;
using DungeonsAndDungeons.Interfaces;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace DungeonsAndDungeons.Entities
{
    public class Player : Entity
    {
        public int Rotation { get; set; }
        private EmptyCommand EmptyCommand { get; }
        private Dictionary<string, Type> Inputs { get; }
        public Player(Vector2 position, Vector2 direction, Inventory inventory, Health health, List<Sprite> stance, ActionPoints ap, EntityState state = EntityState.IDLE)
            : base(position, direction, inventory, health, stance, ap, state)
        {
            EmptyCommand = new EmptyCommand(null, null, null);
            Inputs = new Dictionary<string, Type>()
            {
                { "FINISH_TURN", typeof(FinishTurnCommand) },
                { "MOVE_FORWARD", typeof(MoveForwardCommand) },
                { "ROTATE_LEFT", typeof(RotateLeftCommand) },
                { "ROTATE_RIGHT", typeof(RotateRightCommand) },
                { "MOVE_BACKWARD", typeof(MoveBackwardCommand) },
                { "PICKUP_ITEM", typeof(PickUpItemCommand) },
                { "DROP_ITEM", typeof(DropItemCommand) },
                { "PREVIOUS_SLOT", typeof(PreviousSlotCommand) },
                { "NEXT_SLOT", typeof(NextSlotCommand) }
            };
        }

        public override ICommand GetAction(Level level, GameContext ctx)
        {
            ICommand cmd = EmptyCommand;

            foreach (KeyValuePair<string, Type> entry in Inputs)
            {
                if (InputState.HasAction(entry.Key))
                {
                    cmd = CommandFactory.Generate(entry.Value, this, level, ctx);
                    if (cmd.CanRun())
                    {
                        break;
                    }
                    else
                    {
                        cmd = EmptyCommand;
                    }
                }
            }

            return cmd;
        }

        public override void Update()
        {
        }

    }
}
