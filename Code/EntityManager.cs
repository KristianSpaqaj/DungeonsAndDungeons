using System.Collections.Generic;
using System.Linq;

namespace DungeonsAndDungeons.Code
{
    public static class EntityManager //burde måske være singleton 
    {
        public static List<Entity> Entities { get; set; }

        public static Entity GetEntityById(int id)
        {
            return Entities.Single((ent) => ent.Id == id);
        }
    }
}
