using System;
using System.Collections.Generic;
using System.Linq;
using Project.Configs.Enemies;
using Project.Interfaces.Enemies;
using Project.Systems.Quests;
using Project.Utils.Extensions;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.Spawner
{
    public abstract class BaseEnemySpawner : MonoBehaviour
    {
        private const float BoundsMultiplier = 1.5f;
        private const int MaxTries = 10;

        [SerializeField] private QuestEnemyMark _questMark;
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private LayerMask _obstaclesMask;
        [SerializeField] private float _spawnRadius;

        private readonly List<IPoolableEnemy> _enemies = new();

        private EnemyFactory _enemyFactory;

        public event Action<EnemyConfig> EnemyDied;

        protected bool HasAliveEnemies => _enemies.Any(e => e.IsAlive);

        public EnemyConfig EnemyType => _enemyConfig;

        private void OnDestroy()
        {
            foreach (IPoolableEnemy enemy in _enemies)
                enemy.Died -= OnEnemyDied;
        }

        public abstract void Prepare();

        public void PrepareForQuest()
        {
            OnPrepareForQuest();
            SetQuestMarks();
        }

        public void ReleaseFromQuest()
        {
            foreach (IPoolableEnemy enemy in _enemies)
            {
                QuestEnemyMark mark = enemy.Transform.GetComponentInChildren<QuestEnemyMark>();

                if (mark != null)
                    Destroy(mark.gameObject);
            }
        }

        protected virtual void OnPrepareForQuest()
        {
        }

        protected virtual void OnEnemyDied(IEnemy enemy)
        {
            EnemyDied?.Invoke(_enemyConfig);
        }

        [Inject]
        private void Construct(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
        }

        protected void Spawn()
        {
            Vector3 enemyPosition = GetSpawnPosition();
            IPoolableEnemy enemy = _enemyFactory.Create(_enemyConfig, enemyPosition, transform);

            _enemies.Add(enemy);
            enemy.Died += OnEnemyDied;
        }

        protected Vector3 GetSpawnPosition()
        {
            Vector3 position = GetRandomSpawnPosition();
            Bounds shipBounds = _enemyConfig.View.ShipBounds;

            for (int i = 0; i < MaxTries; i++)
            {
                bool hasObstacles = Physics.CheckBox(position, shipBounds.extents * BoundsMultiplier, Quaternion.identity, _obstaclesMask);

                if (hasObstacles == false)
                    break;

                position = GetRandomSpawnPosition();
            }

            return position;
        }

        private void SetQuestMarks()
        {
            foreach (IPoolableEnemy enemy in _enemies)
            {
                float shipHeight = _enemyConfig.View.ShipBounds.max.y;
                Vector3 markPosition = enemy.Position.AddY(shipHeight);
                Instantiate(_questMark, markPosition, Quaternion.identity, enemy.Transform);
            }
        }

        private Vector3 GetRandomSpawnPosition()
            => (transform.position + Random.insideUnitSphere * _spawnRadius).WithZeroY();

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, _spawnRadius);
        }
    }
}