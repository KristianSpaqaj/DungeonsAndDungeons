﻿using DungeonsAndDungeons.Code.Commands;
using DungeonsAndDungeons.Commands;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DungeonsAndDungeons.Entities
{
    public class Player : Entity
    {
        public int Rotation { get; set; }
        private EmptyCommand EmptyCommand { get; }

        public Player(Vector2 position, Vector2 direction, Inventory inventory, Health health, List<Sprite> stance, ActionPoints ap, EntityState state = EntityState.IDLE)
            : base(position, direction, inventory, health, stance, ap, state)
        {
            Rotation = 0;
            EmptyCommand = new EmptyCommand();
        }

        public override Command GetAction(Level level, GameContext ctx)
        {
            Command cmd = EmptyCommand;

            if (InputState.HasAction("FINISH_TURN"))
            {
                cmd = new FinishTurnCommand(this, level, ctx);
            }

            if (InputState.HasAction("MOVE_FORWARD"))
            {
                if (level.Map.IsValid((int)(Position.X + Direction.X), (int)(Position.Y + Direction.Y)))
                {
                    cmd = new MoveCommand(this, level, ctx, true);
                }
            }

            if (InputState.HasAction("ROTATE_LEFT"))
            {
                cmd = new RotateCommand(this, level, ctx, false);
                Rotation = 90;
            }

            if (InputState.HasAction("ROTATE_RIGHT"))
            {
                cmd = new RotateCommand(this, level, ctx, true);
                Rotation = -90;
            }

            if (InputState.HasAction("MOVE_BACKWARD"))
            {
                if (level.Map.IsValid((int)(Position.X + Direction.X), (int)(Position.Y + Direction.Y)))
                {
                    cmd = new MoveCommand(this, level, ctx, false);
                }
            }

            if (InputState.HasAction("PICKUP_ITEM"))
            {
                List<Item> items = level.ItemsAt((int)Position.X, (int)Position.Y);

                if (items.Count > 0)
                {
                    cmd = new PickUpItemCommand(this, level, ctx, items[0]); //todo find way of choosing which item
                }

            }

            if (InputState.HasAction("DROP_ITEM"))
            {
                if (Inventory.Selected != null)
                {
                    cmd = new DropItemCommand(this, level, ctx, Inventory.Selected);
                }
            }


            if (InputState.HasAction("HEALTHDOWN"))
            {
                cmd = new HealthDownCommand(this, level, ctx, 10);
            }

            List<string> matches = InputState.Find(new System.Text.RegularExpressions.Regex(@"^SLOT\d+$"));

            if (matches.Count > 0)
            {
                cmd = new SelectInventorySlotCommand(this, level, ctx);
                string s = matches[0].Substring(4);
                ((SelectInventorySlotCommand)cmd).Slot = int.Parse(s) - 1;
            }
            return cmd;
        }

        public override void Update()
        {
        }

    }
}
