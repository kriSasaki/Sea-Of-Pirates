using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DTT.Utils.Extensions;
using Scripts.Configs.Game;
using Scripts.Interfaces.Data;
using Scripts.Systems.Quests;
using YG;

namespace Scripts.Systems.Data
{
    public class GameDataService : IResourceStorageData,
        IPlayerStatsData,
        IQuestsData,
        IAdvertismentData,
        ILevelSceneService,
        ILevelDataService,
        IScoreService
    {
        private const int SaveDelay = 1000;
        private const string SaveKey = nameof(SaveKey);
        private bool _isSaving = false;

        public GameDataService(GameConfig config)
        {
            if (GameData == null)
            {
                YandexGame.savesData.GameData = new GameData(config.FirstLevelScene);
                Save();
            }

            if (GameData.CurrentScene.IsNullOrEmpty())
            {
                UpdateCurrentLevel(config.FirstLevelScene);
                Save();
            }
        }

        public string CurrentLevel => GameData.CurrentScene;

        public bool IsAdHided { get => GameData.IsAddHided; }

        private GameData GameData => YandexGame.savesData.GameData;

        public GameResourceData GetResourceData(string id)
        {
            return GameData.GetResourceData(id);
        }

        public PlayerStatData GetPlayerStatData(StatType statType)
        {
            return GameData.GetPlayerStatData(statType);
        }

        public Dictionary<string, QuestStatus> GetQuests()
        {
            return GameData.GetQuests();
        }

        public LevelData GetLevelData(string levelName)
        {
            return GameData.GetLevelData(levelName);
        }

        public int GetScore()
        {
            return GameData.GetScore();
        }

        public void SetScore(int score)
        {
            GameData.SetScore(score);
            Save();
        }

        public void UpdateCurrentLevel(string levelName)
        {
            GameData.UpdateCurrentLevel(levelName);
            Save();
        }

        public void UpdateResourceData(string id, int value)
        {
            GameData.UpdateResourceData(id, value);
            Save();
        }

        public void UpdateStatData(StatType type, int level)
        {
            GameData.UpdateStatData(type, level);
            Save();
        }

        public void UpdateQuestData(string id, QuestStatus status)
        {
            GameData.UpdateQuestData(id, status);
            Save();
        }

        public void RemoveAd()
        {
            GameData.RemoveAd();
            Save();
        }

        public void Save()
        {
            if (_isSaving)
                return;

            SaveAsync().Forget();
        }

        private async UniTaskVoid SaveAsync()
        {
            _isSaving = true;

            await UniTask.Delay(SaveDelay, cancellationToken: YandexGame.Instance.destroyCancellationToken);

            YandexGame.SaveProgress();
            _isSaving = false;
        }
    }
}