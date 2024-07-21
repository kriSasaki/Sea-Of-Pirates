using System;
using System.Collections.Generic;
using System.Linq;
using Project.Interfaces.Data;
using Project.Systems.Stats;

namespace Project.Systems.Data
{
    public class PlayerStatsProvider : IPlayerStatsProvider
    {
        private readonly IPlayerStatsData _statsData;
        private readonly StatsSheet _statsSheet;

        public PlayerStatsProvider(IPlayerStatsData statsData, StatsSheet statsSheet)
        {
            _statsData = statsData;
            _statsSheet = statsSheet;
        }

        public Dictionary<StatType, PlayerStat> LoadStats()
        {
            Dictionary<StatType, int> statsLevels = GetStatsLevels();
            Dictionary<StatType, PlayerStat> stats = new();

            foreach (StatType statType in statsLevels.Keys)
            {
                stats.Add(statType, CreateStat(statType, statsLevels[statType]));
            }

            return stats;
        }

        public void UpdateStats(Dictionary<StatType, PlayerStat> stats)
        {
            foreach (StatType statType in stats.Keys)
            {
                PlayerStatData data = _statsData.StatsLevels.FirstOrDefault(s => s.StatType == statType);

                if (data != null)
                {
                    data.Level = stats[statType].Level;
                }
                else
                {
                    _statsData.StatsLevels.Add(new PlayerStatData() { StatType = statType, Level = stats[statType].Level });
                }
            }

            _statsData.Save();
        }

        private Dictionary<StatType, int> GetStatsLevels()
        {
            Dictionary<StatType, int> statsLevels = new();

            foreach (StatType statType in Enum.GetValues(typeof(StatType)).Cast<StatType>())
            {
                statsLevels.Add(statType, 0);
            }
            foreach (PlayerStatData statData in _statsData.StatsLevels)
            {
                statsLevels[statData.StatType] = statData.Level;
            }

            return statsLevels;
        }

        private PlayerStat CreateStat(StatType type, int level)
        {
            return new PlayerStat(_statsSheet.GetStatConfig(type), level);
        }
    }
}