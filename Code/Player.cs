using DungeonsAndDungeons.Code;
using DungeonsAndDungeons.Commands;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace DungeonsAndDungeons
{
    public class Player : Entity
    {
        public int Rotation { get; set; } // used for camera. TODO get rid of this
        public Player(Vector2 position, Vector2 direction, Inventory inventory, double health, List<Sprite> stance, EntityState state = EntityState.IDLE) : base(position, direction, inventory, health, stance, state)
        {
            Rotation = 0;
        }

        public override Command GetAction(Level level, GameContext ctx)
        {
            Rotation = 0;
            Command cmd = null;

            if (InputState.HasAction("W"))
            {
                cmd = new MoveCommand(this, level, ctx, true);
            }

            if (InputState.HasAction("A"))
            {
                cmd = new RotateCommand(this, level, ctx, false);
            }

            if (InputState.HasAction("D"))
            {
                cmd = new RotateCommand(this, level, ctx, true);
            }

            if (InputState.HasAction("S"))
            {
                cmd = new MoveCommand(this, level, ctx, false);
            }

            return cmd;
        }

    }
}
