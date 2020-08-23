using DungeonsAndDungeons.Entities;
using Microsoft.Xna.Framework;
using System;
using System.Dynamic;

namespace DungeonsAndDungeons.Commands
{
    public class OpenDoorCommand : Command
    {
        public OpenDoorCommand(Entity entity, Level level, GameContext ctx) : base(entity, level, ctx)
        {
        }

        public override void Execute()
        {
            Level.Map[Creator.Position + Creator.Direction] = Level.Map.EmptyTile;
        }

        public override bool CanRun()
        {
            int tile = Level.Map[Creator.Position+Creator.Direction];
            return tile == Level.Map.DoorTile;
        }
    }
}
