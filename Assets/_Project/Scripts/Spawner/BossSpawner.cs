using Cysharp.Threading.Tasks;
using Scripts.Interfaces.Enemies;

namespace Scripts.Spawner
{
    public class BossSpawner : BaseEnemySpawner
    {
        public bool IsBossSpawned => HasAliveEnemies;

        public override void Prepare()
        {
        }

        protected override void OnPrepareForQuest()
        {
            Spawn();
        }

        protected override void OnEnemyDied(IEnemy enemy)
        {
            base.OnEnemyDied(enemy);

            IPoolableEnemy poolEnemy = enemy as IPoolableEnemy;
            poolEnemy.SinkAsync().Forget();
        }
    }
}