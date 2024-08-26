using Project.Configs.Enemies;
using Project.Enemies;
using UnityEngine;

namespace Project.Spawner
{
    [System.Serializable]
    public class EnemyFactory
    {
        [SerializeField] private Enemy _prefab;

        [SerializeField] private VfxSpawner _vfxSpawner;

        public Enemy Create(EnemyConfig enemyConfig, Vector3 position)
        {
            Enemy enemy = Object.Instantiate(_prefab, position, Quaternion.identity);
            enemy.Initialize(enemyConfig, _vfxSpawner);

            return enemy;
        }
    }
}