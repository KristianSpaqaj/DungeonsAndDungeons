using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDungeons.Code
{
    public static class EntityManager //burde måske være singleton 
    {
        public static List<Entity> Entities { get; }

        public static Entity GetEntityById(int id)
        {
            return Entities.Single((ent) => ent.Id == id);
        }
    }
}
