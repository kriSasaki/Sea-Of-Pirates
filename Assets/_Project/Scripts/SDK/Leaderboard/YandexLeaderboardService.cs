using System;
using System.Collections.Generic;
using Scripts.Interfaces.SDK;
using YG;

namespace Scripts.SDK.Leaderboard
{
    public class YandexLeaderboardService : ILeaderboardService
    {
        private const string LeaderboardName = "Leaderboard";

        public bool IsPlayerAuthorized => YandexGame.auth;

        public void SetPlayerScore(int score)
        {
            if (!IsPlayerAuthorized)
                return;

            YandexGame.NewLeaderboardScores(LeaderboardName, score);
        }
    }
}