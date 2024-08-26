using System;
using Project.Configs.Enemies;
using Project.Interfaces.Enemies;
using Project.Spawner;
using Project.Systems.Data;
using UnityEngine;

namespace Project.Enemies
{
    public class Enemy : MonoBehaviour, IPoolableEnemy
    {
        private const int MinimumHealth = 0;

        private EnemyConfig _enemyConfig;
        private VfxSpawner _vfxSpawner;
        private int _currentHealth;

        public event Action<IEnemy> Died;

        public int Damage => _enemyConfig.Damage;
        public float AttackInterval => _enemyConfig.AttackInterval;
        public bool IsAlive => _currentHealth > MinimumHealth;

        public Vector3 Position => transform.position;
        public GameResourceAmount Loot => _enemyConfig.Loot;
        public EnemyConfig Config => _enemyConfig;

        public void Initialize(EnemyConfig enemyConfig, VfxSpawner vfxSpawner)
        {
            _enemyConfig = enemyConfig;
            _vfxSpawner = vfxSpawner;
            _currentHealth = enemyConfig.MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _vfxSpawner.SpawnExplosion(transform.position);

            if (!IsAlive)
            {
                Die();
                return;
            }

        }

        public void Respawn(Vector3 atPosition)
        {
            gameObject.SetActive(true);
            transform.position = atPosition;
            _currentHealth = _enemyConfig.MaxHealth;
        }

        private void Die()
        {
            Died?.Invoke(this);
            Deactivate();
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}