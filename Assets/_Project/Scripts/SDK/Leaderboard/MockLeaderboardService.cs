using Project.Interfaces.SDK;
using UnityEngine;

namespace Project.SDK.Leaderboard
{
    public class MockLeaderboardService : ILeaderboardService
    {
        public bool IsPlayerAuthorized { get; private set; } = false;

        public void SetPlayerScore(int score)
        {
            Debug.Log("New score is " +  score);
        }
    }
}