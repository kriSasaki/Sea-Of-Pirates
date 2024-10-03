using System;
using Cysharp.Threading.Tasks;
using Project.Configs.Game;
using Project.Interfaces.SDK;
using Project.UI.Leaderboard;
using UnityEngine.SceneManagement;
using YG;
using Zenject;

namespace Project.Systems.Leaderboard
{
    public class LeaderboardSystem : IInitializable, IDisposable
    {
        private readonly LeaderboardWindow _leaderboardWindow;
        private readonly LeaderboardButton _leaderboardButton;
        private readonly GameConfig _gameConfig;
        private readonly ILeaderboardService _leaderboardService;

        public LeaderboardSystem(
            LeaderboardWindow leaderboardWindow,
            LeaderboardButton leaderboardButton,
            GameConfig gameConfig,
            ILeaderboardService leaderboardService)
        {
            _leaderboardWindow = leaderboardWindow;
            _leaderboardButton = leaderboardButton;
            _gameConfig = gameConfig;
            _leaderboardService = leaderboardService;
        }

        public void Dispose()
        {
            _leaderboardWindow.AuthorizationRequested -= OnAuthorizationRequested;
        }

        public void Initialize()
        {
            _leaderboardWindow.AuthorizationRequested += OnAuthorizationRequested;
            _leaderboardButton.Bind(OnLeaderboardButtonCLicked);
        }

        private void OnAuthorizationRequested()
        {
            _leaderboardService.AuthorizePlayer();

            ReloadSaveAsync().Forget();
        }

        private async UniTaskVoid ReloadSaveAsync()
        {
            await UniTask.WaitUntil(() => YandexGame.auth == true, cancellationToken: _leaderboardWindow.destroyCancellationToken);
            YandexGame.LoadProgress();
            _leaderboardWindow.OpenLoadingPlaceholder();
            await UniTask.Delay(3000, cancellationToken: _leaderboardWindow.destroyCancellationToken);
            SceneManager.LoadScene(_gameConfig.LoadingScene);
        }

        private void OnLeaderboardButtonCLicked()
        {
            if (_leaderboardService.IsPlayerAuthorized)
            {
                _leaderboardWindow.OpenLeaderboardPanel();
            }
            else
            {
                _leaderboardWindow.OpenAuthorizationPanel();
            }
        }
    }
}