using Project.Enemies;
using UnityEngine;

namespace Project.Spawner
{
    [System.Serializable]
    public class EnemyFactory
    {
        [SerializeField] private Enemy _prefab;

        private Enemy _newEnemy;

        public Enemy Create(EnemyConfig enemyConfig, Vector3 centrePosition, float movementRange)
        {
            _newEnemy = Object.Instantiate(_prefab, centrePosition, Quaternion.identity);
            _newEnemy.Initialize(enemyConfig, centrePosition, movementRange);

            return _newEnemy;
        }
    }
}