using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDungeons.Generation
{
    interface IGenerator<T>
    {
        T Generate();
    }
}
