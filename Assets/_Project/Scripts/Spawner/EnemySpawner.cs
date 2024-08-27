using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Project.Configs.Enemies;
using Project.Interfaces.Enemies;
using Project.Utils.Extensions;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private bool _isRespawnable = true;
        [SerializeField] private int _maxEnemies;
        [SerializeField] private float _spawnRadius;
        [SerializeField, Min(3f)] private float _respawnDelay;

        private readonly List<IPoolableEnemy> _enemies = new ();

        private WaitForSeconds _respawnCooldown;

        public event Action<EnemyConfig> EnemyDied; 

        private void Start()
        {
            _respawnCooldown = new WaitForSeconds(_respawnDelay);

            for (int i = 0; i < _maxEnemies; i++)
            {
                Spawn();
            }
        }

        private void OnDestroy()
        {
            foreach (var enemy in _enemies)
                enemy.Died -= OnEnemyDied;
        }

        private void Spawn()
        {
            Vector3 enemyPosition = GetSpawnPosition();
            IPoolableEnemy enemy = _enemyFactory.Create(_enemyConfig, enemyPosition);

            _enemies.Add(enemy);
            enemy.Died += OnEnemyDied;
        }

        private Vector3 GetSpawnPosition()
        {
            return (transform.position + Random.insideUnitSphere * _spawnRadius).WithZeroY();
        }
        
        private void OnEnemyDied(IEnemy enemy)
        {
            EnemyDied?.Invoke(_enemyConfig);

            if (_isRespawnable)
            {
                StartCoroutine(Respawning(enemy as IPoolableEnemy));
            }
        }

        private IEnumerator Respawning(IPoolableEnemy enemy)
        {
            yield return enemy.SinkAsync().ToCoroutine();
            yield return _respawnCooldown;

            enemy.Respawn(GetSpawnPosition());
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;

            Gizmos.DrawWireSphere(transform.position, _spawnRadius);
        }
    }
}