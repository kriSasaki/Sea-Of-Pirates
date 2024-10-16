using System.Collections.Generic;

namespace Scripts.Systems.Data
{
    [System.Serializable]
    public class GameData
    {
        public List<GameResourceData> StorageData = new List<GameResourceData>();
        public List<PlayerStatData> PlayerStatsLevels = new List<PlayerStatData>();
        public List<QuestData> Quests = new List<QuestData>();
        public List<LevelData> Levels = new List<LevelData>();

        public bool IsAddHided = false;
        public string CurrentScene;
        public int Score = 0;

        public GameData(string sceneName)
        {
            CurrentScene = sceneName;
        }
    }
}