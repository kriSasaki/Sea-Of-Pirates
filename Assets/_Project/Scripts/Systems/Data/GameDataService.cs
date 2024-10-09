using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DTT.Utils.Extensions;
using Project.Configs.Game;
using Project.Interfaces.Data;
using YG;

namespace Project.Systems.Data
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
                GameData.CurrentScene = config.FirstLevelScene;
                Save();
            }
        }

        public List<GameResourceData> Storage => GameData.StorageData;

        public List<PlayerStatData> StatsLevels => GameData.PlayerStatsLevels;

        public List<QuestData> Quests => GameData.Quests;

        public string CurrentLevel => GameData.CurrentScene;

        public bool IsAdHided { get => GameData.IsAddHided; set => GameData.IsAddHided = value; }

        private GameData GameData => YandexGame.savesData.GameData;

        public void UpdateCurrentLevel(string levelName)
        {
            GameData.CurrentScene = levelName;
            Save();
        }

        public LevelData GetLevelData(string levelName)
        {
            LevelData leveldata = GameData.Levels.FirstOrDefault(l => l.LevelName == levelName);

            if (leveldata == null)
            {
                leveldata = new LevelData(levelName);

                GameData.Levels.Add(leveldata);
            }

            return leveldata;
        }

        public void Save()
        {
            if (_isSaving)
                return;

            SaveAsync().Forget();
        }

        public int GetScore()
        {
            return GameData.Score;
        }

        public void SetScore(int score)
        {
            GameData.Score = score;
            Save();
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