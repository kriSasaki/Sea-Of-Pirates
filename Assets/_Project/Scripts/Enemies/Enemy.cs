using System;
using Cysharp.Threading.Tasks;
using Project.Configs.Enemies;
using Project.Enemies.View;
using Project.Interfaces.Enemies;
using Project.Spawner;
using Project.Systems.Data;
using UnityEngine;

namespace Project.Enemies
{
    public class Enemy : MonoBehaviour, IPoolableEnemy
    {
        private const int MinimumHealth = 0;

        [SerializeField] BoxCollider _boxCollider;
        [SerializeField] private EnemyView _enemyView;

        private EnemyConfig _config;
        private VfxSpawner _vfxSpawner;
        private int _currentHealth;

        public event Action<IEnemy> Died;

        public int Damage => _config.Damage;
        public float AttackInterval => _config.AttackInterval;
        public bool IsAlive => _currentHealth > MinimumHealth;

        public Vector3 Position => transform.position;
        public GameResourceAmount Loot => _config.Loot;
        public EnemyConfig Config => _config;

        public void Initialize(EnemyConfig config, VfxSpawner vfxSpawner)
        {
            _config = config;
            _vfxSpawner = vfxSpawner;
            _currentHealth = config.MaxHealth;

            config.ShipView.SetShipColliderSize(_boxCollider);
            _enemyView.Initialize(_config.ShipView);
            name = config.name;
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
            _currentHealth = _config.MaxHealth;
            _enemyView.Ressurect();
        }

        private void Die()
        {
            Died?.Invoke(this);
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public async UniTask SinkAsync()
        {
            await _enemyView.DieAsync();
            Deactivate();
        }
    }
}