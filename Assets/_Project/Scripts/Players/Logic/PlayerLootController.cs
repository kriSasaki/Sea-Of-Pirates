using System;
using Scripts.Interfaces.Enemies;
using Scripts.Interfaces.Hold;

namespace Scripts.Players.Logic
{
    public class PlayerLootController : IDisposable
    {
        private readonly IPlayerHold _playerHold;
        private readonly PlayerAttack _playerAttack;

        public PlayerLootController(IPlayerHold playerHold, PlayerAttack playerAttack)
        {
            _playerHold = playerHold;
            _playerAttack = playerAttack;

            _playerAttack.EnemyKilled += OnEnemyKilled;
        }

        public void Dispose()
        {
            _playerAttack.EnemyKilled -= OnEnemyKilled;
        }

        private void OnEnemyKilled(IEnemy enemy)
        {
            _playerHold.AddResource(enemy.Loot);
        }
    }
}