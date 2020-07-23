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
