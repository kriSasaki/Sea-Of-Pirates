using Project.Interfaces.SDK;

namespace Project.SDK.Leaderboard
{
    public class MockLeaderboardService : ILeaderboardService
    {
        public bool IsPlayerAuthorized { get; private set; } = false;

        public void AuthorizePlayer()
        {
            IsPlayerAuthorized = true;
        }

        public void SetPlayerScore(int score)
        {

        }
    }
}