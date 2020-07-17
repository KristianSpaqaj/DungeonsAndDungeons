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

        public Entity()
        {
            //generate ID
        }

       // public abstract Command GetTurnCommand();

    }
}
