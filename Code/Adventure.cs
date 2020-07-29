using DungeonsAndDungeons.Entities;

namespace DungeonsAndDungeons
{
    public class Adventure
    {
        private LevelGenerator Generator { get; }
        private int NumberOfLevels { get; }
        private string Seed { get; }
        private int Difficulty { get; }
        private Entity Player { get; }
    }
}
