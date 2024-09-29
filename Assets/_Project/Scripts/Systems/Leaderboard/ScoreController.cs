using System.Collections;
using Project.Interfaces.Data;
using Project.Interfaces.Enemies;
using Project.Interfaces.SDK;
using Project.Players.Logic;
using UnityEngine;
using Zenject;

namespace Project.Systems.Leaderboard
{
    public class ScoreController : MonoBehaviour
    {
        private readonly WaitForSeconds _delayTime = new(5f);

        private ILeaderboardService _leaderboardService;
        private IScoreService _scoreService;
        private PlayerAttack _playerAttack;
        private int _killsCount = 0;
        private Coroutine _updateLeaderboardCoroutine;

        private void OnDestroy()
        {
            if (_playerAttack != null)
            {
                _playerAttack.EnemyKilled -= OnEnemyKilled;
            }
        }

        [Inject]
        private void Construct(
            ILeaderboardService leaderboardService,
            IScoreService scoreService,
            PlayerAttack playerAttack)
        {
            _leaderboardService = leaderboardService;
            _scoreService = scoreService;
            _playerAttack = playerAttack;
            _playerAttack.EnemyKilled += OnEnemyKilled;

            _killsCount = _scoreService.GetScore();

            _updateLeaderboardCoroutine = null;
        }

        private void OnEnemyKilled(IEnemy enemy)
        {
            _killsCount++;
            UpdateLeaderboardScoreDelayed();
        }

        private void UpdateLeaderboardScoreDelayed()
        {
            _scoreService.SetScore(_killsCount);

            if (_updateLeaderboardCoroutine != null)
            {
                StopCoroutine(_updateLeaderboardCoroutine);
            }

            _updateLeaderboardCoroutine = StartCoroutine(UpdateLeaderboardScoreAfterDelay());
        }

        private IEnumerator UpdateLeaderboardScoreAfterDelay()
        {
            yield return _delayTime;
            _leaderboardService.SetPlayerScore(_killsCount);
        }
    }
}