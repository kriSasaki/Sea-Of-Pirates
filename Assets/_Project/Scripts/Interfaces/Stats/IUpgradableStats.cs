using Scripts.Systems.Stats;

namespace Scripts.Interfaces.Stats
{
    public interface IUpgradableStats
    {
        int GetStatLevel(StatType type);

        int GetStatValue(StatType type);

        void UpgradeStat(StatType type);
    }
}