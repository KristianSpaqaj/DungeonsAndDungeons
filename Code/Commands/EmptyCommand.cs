using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDungeons.Commands
{
    public class EmptyCommand : Command // for testing purposes only
    {
        public EmptyCommand() : base(null, null, null)
        {
        }

        public override void Execute()
        {
        }
    }
}
