using System;
using System.Collections;
using System.Collections.Generic;
using Project.Enemies;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Project.Spawner
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private EnemyFactory _enemyFactory;
        [SerializeField] private bool _isRespawnable;
        [SerializeField] private int _maxEnemies;
        [SerializeField] private float _spawnRadius;
        [SerializeField] private float _spawnDelay;

        public event Action<EnemyConfig> EnemyDied; 
        
        private List<Enemy> _enemies = new List<Enemy>();
        private Enemy _enemy;

        private void Start()
        {
            for (int i = 0; i < _maxEnemies; i++)
            {
                Spawn();
            }
        }

        private void Spawn()
        {
            _enemy = _enemyFactory.Create(_enemyConfig, transform.position);
            _enemies.Add(_enemy);
            _enemy.Died += OnEnemyDied;
            SetPosition(_enemy);
        }
        
        private void Respawn(Enemy enemy)
        {
            enemy.Restore();
            SetPosition(enemy);
        }

        private void SetPosition(Enemy enemy)
        {
            enemy.transform.position = transform.position + Random.insideUnitSphere * _spawnRadius;
            enemy.transform.position = new Vector3(
                enemy.transform.position.x,
                transform.position.y,
                enemy.transform.position.z);
        }
        
        private void OnEnemyDied(Enemy enemy)
        {
            EnemyDied?.Invoke(_enemyConfig);

            if (_isRespawnable)
            {
                StartCoroutine(WaitUntilSpawn(enemy));
            }
        }

        private IEnumerator WaitUntilSpawn(Enemy enemy)
        {
            yield return new WaitForSeconds(_spawnDelay);
            Respawn(enemy);
        }
    }
}