using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DTT.Utils.Extensions;
using Scripts.Configs.Game;
using Scripts.Interfaces.Data;
using Scripts.UI.Leaderboard;
using UnityEngine.SceneManagement;
using YG;
using Zenject;

namespace Scripts.Systems.Leaderboard
{
    public class AuthorizationHandler : IInitializable, IDisposable
    {
        private readonly LeaderboardWindow _leaderboardWindow;
        private readonly GameConfig _gameConfig;
        private readonly ILevelSceneService _levelSceneService;

        public AuthorizationHandler(
            LeaderboardWindow leaderboardWindow,
            GameConfig gameConfig,
            ILevelSceneService levelSceneService)
        {
            _leaderboardWindow = leaderboardWindow;
            _gameConfig = gameConfig;
            _levelSceneService = levelSceneService;
        }

        public void Initialize()
        {
            _leaderboardWindow.AuthorizationRequested += OnAuthorizationRequested;
        }

        public void Dispose()
        {
            _leaderboardWindow.AuthorizationRequested -= OnAuthorizationRequested;
        }

        private void OnAuthorizationRequested()
        {
            YandexGame.AuthDialog();
            ReloadGameAsync().Forget();
        }

        private async UniTaskVoid ReloadGameAsync()
        {
            CancellationToken token = _leaderboardWindow.destroyCancellationToken;

            await UniTask.WaitUntil(() => YandexGame.auth == true, cancellationToken: token);

            YandexGame.ProgressLoaded += OnProgressLoaded;
            YandexGame.LoadProgress();

            _leaderboardWindow.OpenLoadingPlaceholder();

            YandexGame.GameplayStop();
        }

        private void OnProgressLoaded()
        {
            YandexGame.ProgressLoaded -= OnProgressLoaded;

            if (_levelSceneService.CurrentLevel.IsNullOrEmpty())
            {
                _levelSceneService.UpdateCurrentLevel(_gameConfig.FirstLevelScene);
            }

            SceneManager.LoadScene(_gameConfig.LoadingScene);
        }
    }
}