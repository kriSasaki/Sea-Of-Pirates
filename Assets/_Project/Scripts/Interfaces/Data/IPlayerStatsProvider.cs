using System.Collections.Generic;
using Scripts.Systems.Stats;

namespace Scripts.Interfaces.Data
{
    public interface IPlayerStatsProvider
    {
        public Dictionary<StatType, PlayerStat> LoadStats();

        public void UpdateStats();
    }
}