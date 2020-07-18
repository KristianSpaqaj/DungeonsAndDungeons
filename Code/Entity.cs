using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDungeons
{
    public class Entity
    {
        public int Id { get; }
        public Dictionary<string, object> Attributes { get; set; }
        public Behaviour Behaviour {get;set;}

        public Entity()
        {
            //generate ID
        }

        public Command GetTurnCommand(Level level)
        {
            return Behaviour.Run(Id, level);
        }

    }
}
