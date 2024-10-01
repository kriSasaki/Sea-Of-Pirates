using System;
using System.Collections.Generic;
using Agava.YandexGames;
using Project.Interfaces.SDK;
using YG;

namespace Project.SDK.Leaderboard
{
    public class YandexLeaderboardService : ILeaderboardService
    {
        private const string LeaderboardName = "Leaderboard";

        private readonly List<LeaderboardPlayer> _leaderboardPlayers = new();

        public bool IsPlayerAuthorized => YandexGame.auth;

        public void SetPlayerScore(int score)
        {
            if (!IsPlayerAuthorized)
                return;

            YandexGame.NewLeaderboardScores(LeaderboardName, score);
        }

        public void AuthorizePlayer()
        {
            YandexGame.RequestAuth();
            //PlayerAccount.Authorize();
        }

        public void LoadPlayers(Action<List<LeaderboardPlayer>, int> onLoadCallback)
        {
            if (!IsPlayerAuthorized)
                return;

            _leaderboardPlayers.Clear();

            int playerRank = 0;

            Agava.YandexGames.Leaderboard.GetPlayerEntry(LeaderboardName, (result) =>
            {
                playerRank = result.rank;

                Agava.YandexGames.Leaderboard.GetEntries(LeaderboardName, (result) =>
                {
                    foreach (var entry in result.entries)
                    {
                        int rank = entry.rank;
                        int score = entry.score;
                        string name = entry.player.publicName;

                        _leaderboardPlayers.Add(new LeaderboardPlayer(rank, name, score));
                    }

                    onLoadCallback(_leaderboardPlayers, playerRank);
                });
            });
        }
    }
}