using System.Collections.Generic;
using Project.Systems.Data;

namespace Project.Interfaces.Data
{
    public interface IPlayerStatsData : ISaveable
    {
        List<PlayerStatData> StatsLevels { get; }
    }
}