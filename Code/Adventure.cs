using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonsAndDungeons
{
    class Adventure
    {
        private LevelGenerator Generator { get; set; }
        private int NumberOfLevels { get; set; }
        private string Seed { get; set; }
        private int Difficulty { get; set; }
        private Entity Player { get; set; }
    }
}
