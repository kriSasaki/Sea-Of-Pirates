using System;
using Project.Interfaces.Enemies;
using Project.Interfaces.Hold;

namespace Project.Players.PlayerLogic
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