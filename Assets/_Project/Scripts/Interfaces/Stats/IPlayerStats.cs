using System;

namespace Scripts.Interfaces.Stats
{
    public interface IPlayerStats
    {
        public event Action StatsUpdated;

        int MaxHealth { get; }
        int Damage { get; }
        int CargoSize { get; }
        int Speed { get; }
        int AttackRange { get; }
        int CannonsAmount { get; }
    }
}