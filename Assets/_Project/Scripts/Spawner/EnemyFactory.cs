using Project.Enemies;
using UnityEngine;

namespace Project.Spawner
{
    [System.Serializable]
    public class EnemyFactory
    {
        [SerializeField] private Enemy _prefab;

        private Enemy _newEnemy;

        public Enemy Create(EnemyConfig enemyConfig, Vector3 position)
        {
            _newEnemy = Object.Instantiate(_prefab, position, Quaternion.identity);
            _newEnemy.Initialize(enemyConfig);

            return _newEnemy;
        }
    }
}