namespace Project.Interfaces.SDK
{
    public interface ILeaderboardService
    {
        bool IsPlayerAuthorized { get; }

        void SetPlayerScore(int score);
    }
}