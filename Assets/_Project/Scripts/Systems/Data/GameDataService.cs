﻿using System.Collections.Generic;
using System.Linq;
using Project.Configs.Game;
using Project.Configs.Level;
using Project.Interfaces.Data;
using UnityEngine;

namespace Project.Systems.Data
{
    public class GameDataService : IResourceStorageData,
        IPlayerStatsData,
        IQuestsData,
        IAdvertismentData,
        ILevelSceneService,
        ILevelDataService
    {
        private const string SaveKey = nameof(SaveKey);

        private readonly GameData _gameData;

        public GameDataService(GameConfig config)
        {
            GameData data = JsonUtility.FromJson<GameData>(PlayerPrefs.GetString(SaveKey, null));

            _gameData = data ?? new GameData(config.FirstLevelScene);
        }

        public List<GameResourceData> Storage => _gameData.StorageData;

        public List<PlayerStatData> StatsLevels => _gameData.PlayerStatsLevels;

        public List<QuestData> Quests => _gameData.Quests;

        public string CurrentLevel => _gameData.CurrentScene;

        public bool IsAdActive { get => _gameData.IsAddActive; set => _gameData.IsAddActive = value; }

        public void UpdateCurrentLevel(string levelName)
        {
            _gameData.CurrentScene = levelName;
            Save();
        }

        public LevelData GetLevelData()
        {
            LevelData leveldata = _gameData.Levels.FirstOrDefault(l=> l.LevelName == CurrentLevel);

            if (leveldata == null)
            {
                leveldata = new LevelData(CurrentLevel);

                _gameData.Levels.Add(leveldata);
            }

            return leveldata;
        }

        public void Save()
        {
            string json = JsonUtility.ToJson(_gameData);

            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
        }
    }
}