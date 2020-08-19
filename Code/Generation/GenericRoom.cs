using DungeonsAndDungeons.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDungeons.Generation
{
    public class GenericRoom : Room
    {
        public GenericRoom(int x, int y, int height, int width, int type) : base(x, y, height, width, type){}
    }
}
