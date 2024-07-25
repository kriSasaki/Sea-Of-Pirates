using System;
using System.Collections.Generic;
using Project.Enemies;
using Project.Interfaces.Enemies;

namespace Project.Spawner
{
    public class EnemyDeathNotifier: IEnemyDeathNotifier, IDisposable
    {
        private readonly List<EnemySpawner> _enemySpawners;

        public event Action<EnemyConfig> EnemyDied;

        public EnemyDeathNotifier(List<EnemySpawner> enemySpawners)
        {
            _enemySpawners = enemySpawners;

            foreach (var enemySpawner in _enemySpawners)
            {
                enemySpawner.EnemyDied += OnEnemyDied;
            }
        }

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