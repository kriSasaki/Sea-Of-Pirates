using System;
using System.Collections.Generic;
using Project.Interfaces.SDK;
using Project.SDK.Leaderboard;
using Project.UI.Leaderboard;
using Zenject;

namespace Project.Systems.Leaderboard
{
    public class LeaderboardSystem : IInitializable, IDisposable
    {
        private readonly LeaderboardWindow _leaderboardWindow;
        private readonly LeaderboardButton _leaderboardButton;
        private readonly ILeaderboardService _leaderboardService;

        public LeaderboardSystem(
            LeaderboardWindow leaderboardWindow,
            LeaderboardButton leaderboardButton,
            ILeaderboardService leaderboardService)
        {
            _leaderboardWindow = leaderboardWindow;
            _leaderboardButton = leaderboardButton;
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