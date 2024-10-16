using System.Collections.Generic;
using System.Linq;
using Scripts.Systems.Quests;

namespace Scripts.Systems.Data
{
    [System.Serializable]
    public class GameData
    {
        private const int InitialStatLevel = 1;

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

        public GameResourceData GetResourceData(string id)
        {
           return StorageData.FirstOrDefault(r => r.ID == id);
        }

        public void UpdateResourceData(string id, int Value)
        {
            GameResourceData data = StorageData.FirstOrDefault(r => r.ID == id);

            if (data != null)
            {
                data.Value = Value;
            }
            else
            {
                StorageData.Add(new GameResourceData() { ID = id, Value = Value });
            }
        }

        public PlayerStatData GetPlayerStatData(StatType statType)
        {
            PlayerStatData playerStatData = PlayerStatsLevels.FirstOrDefault(s => s.StatType == statType);

            if (playerStatData == null)
            {
                playerStatData = new PlayerStatData(statType, InitialStatLevel);
                PlayerStatsLevels.Add(playerStatData);
            }

            return playerStatData;
        }

        public void UpdateStatData(StatType type, int level)
        {
            PlayerStatData data = PlayerStatsLevels.FirstOrDefault(s => s.StatType == type);

            if (data != null)
            {
                data.Level = level;
            }
            else
            {
                var statData = new PlayerStatData(type, level);
                PlayerStatsLevels.Add(statData);
            }
        }

        public Dictionary<string, QuestStatus> GetQuests()
        {
            var quests = new Dictionary<string, QuestStatus>();

            foreach (QuestData questData in Quests)
            {
                quests.Add(questData.ID, new QuestStatus(questData.State, questData.Progress));
            }

            return quests;
        }

        public void UpdateQuestData(string id, QuestStatus status)
        {
            if (Quests.Any(q => q.ID == id) == false)
            {
                Quests.Add(new QuestData(id, status));
            }

            QuestData data = Quests.Find(q => q.ID == id);
            data.Update(status);
        }

        public LevelData GetLevelData(string levelName)
        {
            LevelData leveldata = Levels.FirstOrDefault(l => l.LevelName == levelName);

            if (leveldata == null)
            {
                leveldata = new LevelData(levelName);

               Levels.Add(leveldata);
            }

            return leveldata;
        }

        public void RemoveAd()
        {
            IsAddHided = true;
        }

        public void UpdateCurrentLevel(string levelName)
        {
            CurrentScene = levelName;
        }

        public void SetScore(int score)
        {
            Score = score;
        }

        public int GetScore()
        {
            return Score;
        }
    }
}