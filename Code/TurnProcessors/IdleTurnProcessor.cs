using DungeonsAndDungeons.Commands;
using DungeonsAndDungeons.Entities;
using DungeonsAndDungeons.Interfaces;
using System.Collections.Generic;

namespace DungeonsAndDungeons.TurnProcessors
{
    public class IdleTurnProcessor : TurnProcessor
    {
        public IdleTurnProcessor() : base(0.25)
        {
        }
    }
}
