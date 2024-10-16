using System;
using System.Collections.Generic;
using Scripts.Configs.Enemies;
using Scripts.Interfaces.Enemies;
using Scripts.Spawner;

namespace Scripts.Enemies
{
    public class EnemyDeathNotifier : IEnemyDeathNotifier, IDisposable
    {
        private readonly List<BaseEnemySpawner> _enemySpawners;

        public EnemyDeathNotifier(List<BaseEnemySpawner> enemySpawners)
        {
            _enemySpawners = enemySpawners;

            foreach (var enemySpawner in _enemySpawners)
            {
                enemySpawner.EnemyDied += OnEnemyDied;
            }
        }

        public event Action<EnemyConfig> EnemyDied;

        public void Dispose()
        {
            foreach (var enemySpawner in _enemySpawners)
            {
                enemySpawner.EnemyDied -= OnEnemyDied;
            }
        }

        private void OnEnemyDied(EnemyConfig enemyConfig)
        {
            EnemyDied?.Invoke(enemyConfig);
        }
    }
}