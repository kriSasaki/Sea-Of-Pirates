namespace Project.Interfaces.Stats
{
    public interface IPlayerStats
    {
        int MaxHealth { get; }
        int Damage { get; }
        int CargoSize { get; }
        int Speed { get; }
        int AttackRange { get; }
        int CannonsAmount { get; }
    }
}