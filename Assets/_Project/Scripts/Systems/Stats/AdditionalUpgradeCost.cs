using Scripts.Utils;
using UnityEngine;

namespace Scripts.Systems.Data
{
    [System.Serializable]
    public class AdditionalUpgradeCost : UpgradeCost
    {
        [SerializeField] private int _firstApplicationLevel;
        [SerializeField] private int _applicationInterval;

        public bool IsApplicable(int level)
        {
            return level >= _firstApplicationLevel && IsApplicableLevel(level);
        }

        protected override int ComputeCost(StatConfig statConfig, int level)
        {
            return (int)ExtendedMath.Remap(level, _firstApplicationLevel, statConfig.MaxLevel, MinCost, MaxCost);
        }

        private bool IsApplicableLevel(int level)
        {
            return (level - _firstApplicationLevel) % _applicationInterval == 0;
        }
    }
}