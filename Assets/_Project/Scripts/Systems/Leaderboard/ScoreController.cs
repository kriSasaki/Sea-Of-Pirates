using System.Collections;
using Scripts.Interfaces.Data;
using Scripts.Interfaces.Enemies;
using Scripts.Interfaces.SDK;
using Scripts.Players.Logic;
using UnityEngine;
using Zenject;

namespace Scripts.Systems.Leaderboard
{
    public class ScoreController : MonoBehaviour
    {
        private readonly WaitForSeconds _delayTime = new WaitForSeconds(2f);

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