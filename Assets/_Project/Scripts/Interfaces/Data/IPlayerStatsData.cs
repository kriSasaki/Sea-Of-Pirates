using System.Collections.Generic;
using Scripts.Systems.Data;

namespace Scripts.Interfaces.Data
{
    public interface IPlayerStatsData : ISaveable
    {
        List<PlayerStatData> StatsLevels { get; }
    }
}