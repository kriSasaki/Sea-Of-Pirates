using System.Collections.Generic;
using Project.Systems.Stats;

namespace Project.Interfaces.Data
{
    public interface IPlayerStatsProvider
    {
        public Dictionary<StatType, PlayerStat> LoadStats();
        public void UpdateStats(Dictionary<StatType, PlayerStat> stats);
    }
}