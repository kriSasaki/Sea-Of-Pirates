using Project.Interfaces.SDK;
using Project.UI.Leaderboard;
using Zenject;

namespace Project.Systems.Leaderboard
{
    public class LeaderboardSystem : IInitializable
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

        public void Initialize()
        {
            _leaderboardButton.Bind(OnLeaderboardButtonCLicked);
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