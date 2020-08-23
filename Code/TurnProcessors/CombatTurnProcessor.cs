using DungeonsAndDungeons.Commands;
using DungeonsAndDungeons.Entities;
using DungeonsAndDungeons.Interfaces;
using System;
using System.Collections.Generic;

namespace DungeonsAndDungeons.TurnProcessors
{
    class CombatTurnProcessor : TurnProcessor
    {
        public CombatTurnProcessor() : base(0.25)
        {
            //InvalidCommands.Add(typeof(OpenDoorCommand));
        }

    }
}
