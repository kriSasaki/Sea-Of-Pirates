using System;
using Agava.YandexGames.Utility;
using Project.Interfaces.Enemies;
using Project.Interfaces.SDK;
using Project.Players.Logic;
using Zenject;

namespace Project.Controllers
{
    public class ScoreController : IDisposable
    {
        private const string SaveNumberEnemysKilled = "SaveNumberEnemysKilled";
        private ILeaderboardService _leaderboardService;
        private PlayerAttack _playerAttack;
        private int _killsCount = 0;

        private void Start()
        {
            if (PlayerPrefs.HasKey(SaveNumberEnemysKilled))
            {
                _killsCount = PlayerPrefs.GetInt(SaveNumberEnemysKilled);
            }
        }

        [Inject]
        private void Construct(ILeaderboardService leaderboardService, PlayerAttack playerAttack)
        {
            _leaderboardService = leaderboardService;
            _playerAttack = playerAttack;
            _playerAttack.EnemyKilled += OnEnemyKilled;
        }

        private void OnEnemyKilled(IEnemy enemy)
        {
            _killsCount++;
            UpdateLeaderboardScore();
        }

        private void UpdateLeaderboardScore()
        {
            _leaderboardService.SetPlayerScore(_killsCount);
            PlayerPrefs.SetInt(SaveNumberEnemysKilled, _killsCount);
        }

        public void Dispose()
        {
            if (_playerAttack != null)
            {
                _playerAttack.EnemyKilled -= OnEnemyKilled;
            }
        }
    }
}