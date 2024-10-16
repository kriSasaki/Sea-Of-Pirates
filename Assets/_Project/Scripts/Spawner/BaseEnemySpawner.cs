using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Configs.Enemies;
using Scripts.Interfaces.Enemies;
using Scripts.Systems.Quests;
using Scripts.Utils.Extensions;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Scripts.Spawner
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

        public EnemyConfig EnemyType => _enemyConfig;

        protected bool HasAliveEnemies => _enemies.Any(e => e.IsAlive);

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
            Vector3 shipExtents = shipBounds.extents * BoundsMultiplier;

            for (int i = 0; i < MaxTries; i++)
            {
                bool hasObstacles = Physics.CheckBox(position, shipExtents, Quaternion.identity, _obstaclesMask);

                if (hasObstacles == false)
                    break;

                position = GetRandomSpawnPosition();
            }

            return position;
        }

        [Inject]
        private void Construct(EnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
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