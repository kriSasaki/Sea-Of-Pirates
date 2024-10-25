using System.Collections.Generic;
using Scripts.Systems.Data;
using Scripts.Systems.Stats;

namespace Scripts.Interfaces.Data
{
    public interface IPlayerStatsData : ISaveable
    {
        PlayerStatData GetPlayerStatData(StatType statType);

        void UpdateStatData(StatType type, int level);
    }
}