using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDungeons.Generation
{
    public interface IGenerator<T>
    {
        List<T> Generate(Room room, int count);
    }
}
