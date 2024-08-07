using System.Collections.Generic;
using Project.Interfaces.Data;
using UnityEngine;

namespace Project.Systems.Data
{
    public class GameDataService : IResourceStorageData, IPlayerStatsData, IQuestsData, IAdvertismentData
    {
        private const string SaveKey = nameof(SaveKey);

        private readonly GameData _gameData;

        public GameDataService()
        {
            GameData data = JsonUtility.FromJson<GameData>(PlayerPrefs.GetString(SaveKey, null));

            _gameData = data ?? new GameData();
        }

        public List<GameResourceData> Storage => _gameData.StorageData;

        public List<PlayerStatData> StatsLevels => _gameData.PlayerStatsLevels;

        public List<QuestData> Quests => _gameData.Quests;

        public bool IsAddActive { get => _gameData.IsAddActive; set => _gameData.IsAddActive = value; }

        public void Save()
        {
            string json = JsonUtility.ToJson(_gameData);

            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
        }
    }
}