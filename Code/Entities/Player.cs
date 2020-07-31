using DungeonsAndDungeons.Commands;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace DungeonsAndDungeons.Entities
{
    public class Player : Entity
    {
        public Item DrawnItem { get; set; }

        public Player(Vector2 position, Vector2 direction, Inventory inventory, double health, List<Sprite> stance, EntityState state = EntityState.IDLE) : base(position, direction, inventory, health, stance, state)
        {
            DrawnItem = null;
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
            }

            if (InputState.HasAction("ROTATE_RIGHT"))
            {
                cmd = new RotateCommand(this, level, ctx, true);
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
                if (Inventory.Count > 0)
                {
                    cmd = new DropItemCommand(this, level, ctx, Inventory[0]);
                }
            }

            

            return cmd;
        }

        public override void Update()
        {
            if (Inventory.Count > 0)
            {
                DrawnItem = Inventory[0];
            }
            else
            {
                DrawnItem = null;
            }
        }

    }
}
