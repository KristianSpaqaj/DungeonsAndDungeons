using DungeonsAndDungeons.Entities;

namespace DungeonsAndDungeons.Interfaces
{
    public interface ICommand
    {
        Entity Creator { get; }
        Level Level { get; }
        GameContext Context { get; }
        int ActionCost { get; }
        bool TimesOut { get; }

        void Execute();
        bool CanRun();
    }
}
