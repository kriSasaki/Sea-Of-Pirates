using System.Collections.Generic;
using Project.Systems.Data;

namespace Project.Interfaces.Data
{
    public interface IPlayerStatsProvider
    {
        public Dictionary<StatType, PlayerStat> LoadStats();
        public void UpdateStats();
    }
}