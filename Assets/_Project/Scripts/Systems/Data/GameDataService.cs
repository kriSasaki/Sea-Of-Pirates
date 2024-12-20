﻿using System.Collections.Generic;
using Project.Interfaces.Data;
using UnityEngine;

namespace Project.Systems.Data
{
    public class GameDataService : IResourceStorageData
    {
        private const string SaveKey = nameof(SaveKey);

        private readonly GameData _gameData;

        public GameDataService()
        {
            GameData data = JsonUtility.FromJson<GameData>(PlayerPrefs.GetString(SaveKey, null));

            _gameData = data ?? new GameData();
        }

        public List<GameResourceData> Resources => _gameData.Storage;

        public void Save()
        {
            string json = JsonUtility.ToJson(_gameData);

            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();
        }
    }
}