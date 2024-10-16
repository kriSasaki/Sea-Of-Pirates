namespace Scripts.Systems.Data
{
    [System.Serializable]
    public class PlayerStatData
    {
        public StatType StatType;
        public int Level;

        public PlayerStatData(StatType statType, int level)
        {
            StatType = statType;
            Level = level;
        }
    }
}