using DungeonsAndDungeons.Commands;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DungeonsAndDungeons.Entities
{
    public class Player : Entity
    {
        public Item SelectedItem => Inventory[SelectedSlot];
        public int Rotation { get; set; }
        private int SelectedSlot { get; set; }

        public Player(Vector2 position, Vector2 direction, Inventory inventory, Health health, List<Sprite> stance, EntityState state = EntityState.IDLE) : base(position, direction, inventory, health, stance, state)
        {
            Rotation = 0;
            SelectedSlot = 0;
        }

        public override Command GetAction(Level level, GameContext ctx)
        {
            Command cmd = null;

            if (InputState.HasAction("MOVE_FORWARD"))
            {
                cmd = new MoveCommand(this, level, ctx, true);
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
                cmd = new MoveCommand(this, level, ctx, false);
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
                if (SelectedItem != null)
                {
                    cmd = new DropItemCommand(this, level, ctx, SelectedItem);
                }
            }


            if (InputState.HasAction("HEALTHDOWN"))
            {
                cmd = new HealthDownCommand(this, level, ctx, 10);
            }

            return cmd;
        }

        public override void Update()
        {
        }

    }
}
