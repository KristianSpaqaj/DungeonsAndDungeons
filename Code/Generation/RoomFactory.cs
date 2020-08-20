using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDungeons.Generation
{
    public static class RoomFactory
    {
        public static Room Generate(int type, int x, int y, int height, int width)
        {
            switch (type)
            {
                case 1:
                    return new StartRoom(x, y, height, width);
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                    return new GenericRoom(x, y, height, width);
                    break;
                default:
                    throw new ArgumentException("Invalid wall type");
            }
        }
    }
}
