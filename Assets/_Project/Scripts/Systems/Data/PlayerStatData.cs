using UnityEngine;

namespace Scripts.Systems.Data
{
    [System.Serializable]
    public class PlayerStatData
    {
        [SerializeField] private StatType _statType;
        [SerializeField] private int _level;

        public StatType StatType => _statType;
        public int Level => _level;

        public PlayerStatData(StatType statType, int level)
        {
            _statType = statType;
            _level = level;
        }

        public void SetLevel(int level)
        {
            _level = level;
        }
    }
}