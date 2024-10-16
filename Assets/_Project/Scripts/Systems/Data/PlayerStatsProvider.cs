using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Interfaces.Data;

namespace Scripts.Systems.Data
{
    public class PlayerStatsProvider : IPlayerStatsProvider
    {
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
                _statsData.UpdateStatData(statType, _stats[statType].Level);
            }
        }

        private Dictionary<StatType, int> GetStatsLevels()
        {
            Dictionary<StatType, int> statsLevels = new();

            foreach (StatType statType in Enum.GetValues(typeof(StatType)).Cast<StatType>())
            {
                PlayerStatData playerStatData = _statsData.GetPlayerStatData(statType);

                statsLevels.Add(playerStatData.StatType, playerStatData.Level);
            }

            return statsLevels;
        }

        private PlayerStat CreateStat(StatType type, int level)
        {
            return new PlayerStat(_statsSheet.GetStatConfig(type), level);
        }
    }
}