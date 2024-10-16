using System.Collections;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using Scripts.Interfaces.Enemies;
using UnityEngine;
using Zenject;

namespace Scripts.Spawner
{
    public class EnemySpawner : BaseEnemySpawner
    {
        [HorizontalLine(3f, EColor.Blue)]
        [SerializeField, Min(1)] private int _maxEnemies;
        [SerializeField] private bool _isRespawnable = true;
        [SerializeField, ShowIf(nameof(_isRespawnable)), Min(3f)] private float _respawnDelay;

        private WaitForSeconds _respawnCooldown;

        public override void Prepare()
        {
            for (int i = 0; i < _maxEnemies; i++)
            {
                Spawn();
            }
        }

        protected override void OnEnemyDied(IEnemy enemy)
        {
            base.OnEnemyDied(enemy);

            IPoolableEnemy poolEnemy = enemy as IPoolableEnemy;

            if (_isRespawnable)
                StartCoroutine(Respawning(poolEnemy));
            else
                poolEnemy.SinkAsync().Forget();
        }

        [Inject]
        private void Construct(EnemyFactory enemyFactory)
        {
            _respawnCooldown = new WaitForSeconds(_respawnDelay);
        }

        private IEnumerator Respawning(IPoolableEnemy enemy)
        {
            yield return enemy.SinkAsync().ToCoroutine();
            yield return _respawnCooldown;

            enemy.Respawn(GetSpawnPosition());
        }
    }
}