using System;
using System.Collections.Generic;
using Project.Enemies;
using Project.Interfaces.Enemies;
using UnityEngine;

namespace Project.Spawner
{
    public class EnemyDeathNotifier : MonoBehaviour, IEnemyDeathNotifier
    {
        [SerializeField] private List<EnemySpawner> _enemySpawners;

        public event Action<EnemyConfig> EnemyDied;
        
        private void Start()
        {
            foreach (var enemySpawner in _enemySpawners)
            {
                enemySpawner.EnemyDied += OnEnemyDied;
            }
        }

        private void OnEnemyDied(EnemyConfig enemyConfig)
        {
            EnemyDied?.Invoke(enemyConfig);
        }
    }
}