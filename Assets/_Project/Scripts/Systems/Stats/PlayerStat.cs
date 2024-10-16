namespace Scripts.Systems.Data
{
    public class PlayerStat
    {
        public PlayerStat(StatConfig config, int level)
        {
            Config = config;
            Level = level;
        }

        public StatConfig Config { get; }
        public int Level { get; private set; }

        public int GetValue()
        {
            return Config.GetValue(Level);
        }

        public void LevelUp()
        {
            Level++;
        }

        public void SetLevel(int level)
        {
            Level = level;
        }
    }
}