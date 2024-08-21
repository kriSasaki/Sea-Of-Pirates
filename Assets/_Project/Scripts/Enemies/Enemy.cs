using System;
using Project.Configs.GameResources;
using Project.Interfaces.Enemies;
using UnityEngine;

namespace Project.Enemies
{
    public class Enemy : MonoBehaviour, IEnemy
    {
        private const int MinimumHealth = 0;

        public int Damage => _damage;
        public float AttackSpeed => _attackSpeed;
        
        private EnemyConfig _enemyConfig;

        private int _health;
        private int _damage;
        private float _attackSpeed;
        private GameResource _gameResource;
        private int _resourceAmount;

        public event Action<Enemy> Died;

        public void Initialize(EnemyConfig enemyConfig)
        {
            _health = enemyConfig.Health;
            _damage = enemyConfig.Damage;
            _attackSpeed = enemyConfig.AttackSpeed;
            _gameResource = enemyConfig.GameResource;
            _resourceAmount = enemyConfig.ResourceAmount;
        }

        public void TakeDamage(int damage)
        {
            _health -= damage;
            Debug.Log("Enemy take damage: " + damage);

            if (_health <= MinimumHealth)
            {
                Died?.Invoke(this);

                Destroy(gameObject);
            }
        }
    }
}