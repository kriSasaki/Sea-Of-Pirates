using System.Collections.Generic;
using System.Linq;
using Scripts.Systems.Quests;
using UnityEngine;

namespace Scripts.Systems.Data
{
    [System.Serializable]
    public class GameData
    {
        private const int InitialStatLevel = 1;

        [SerializeField] private List<GameResourceData> StorageData = new List<GameResourceData>();
        [SerializeField] private List<PlayerStatData> PlayerStatsLevels = new List<PlayerStatData>();
        [SerializeField] private List<QuestData> Quests = new List<QuestData>();
        [SerializeField] private List<LevelData> Levels = new List<LevelData>();
        [SerializeField] private string _currentScene;
        [SerializeField] private bool _isAddHided = false;
        [SerializeField] private int _score = 0;

        public bool IsAddHided => _isAddHided;
        public string CurrentScene => _currentScene;

        public GameData(string sceneName)
        {
            _currentScene = sceneName;
        }

        public GameResourceData GetResourceData(string id)
        {
           return StorageData.FirstOrDefault(r => r.ID == id);
        }

        public void UpdateResourceData(string id, int value)
        {
            GameResourceData data = StorageData.FirstOrDefault(r => r.ID == id);

            if (data != null)
            {
                data.ChangeResourceAmount(value);
            }
            else
            {
                StorageData.Add(new GameResourceData(id, value));
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
                data.SetLevel(level);
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
            _isAddHided = true;
        }

        public void UpdateCurrentLevel(string levelName)
        {
            _currentScene = levelName;
        }

        public void SetScore(int score)
        {
            _score = score;
        }

        public int GetScore()
        {
            return _score;
        }
    }
}