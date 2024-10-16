using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Interfaces.Data;

namespace Scripts.Systems.Data
{
    public class PlayerStatsProvider : IPlayerStatsProvider
    {
        private const int MinimalStatLevel = 1;

        private readonly IPlayerStatsData _statsData;
        private readonly StatsSheet _statsSheet;

        private Dictionary<StatType, PlayerStat> _stats;

        public PlayerStatsProvider(IPlayerStatsData statsData, StatsSheet statsSheet)
        {
            _statsData = statsData;
            _statsSheet = statsSheet;
            _stats = null;
        }

        public Dictionary<StatType, PlayerStat> LoadStats()
        {
            _stats = new();

            Dictionary<StatType, int> statsLevels = GetStatsLevels();

            foreach (StatType statType in statsLevels.Keys)
            {
                _stats.Add(statType, CreateStat(statType, statsLevels[statType]));
            }

            return _stats;
        }

        public void UpdateStats()
        {
            foreach (StatType statType in _stats.Keys)
            {
                PlayerStatData data = _statsData.StatsLevels.FirstOrDefault(s => s.StatType == statType);

                if (data != null)
                {
                    data.Level = _stats[statType].Level;
                }
                else
                {
                    var statData = new PlayerStatData() { StatType = statType, Level = _stats[statType].Level };
                    _statsData.StatsLevels.Add(statData);
                }
            }

            _statsData.Save();
        }

        private Dictionary<StatType, int> GetStatsLevels()
        {
            Dictionary<StatType, int> statsLevels = new();

            foreach (StatType statType in Enum.GetValues(typeof(StatType)).Cast<StatType>())
            {
                statsLevels.Add(statType, MinimalStatLevel);
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