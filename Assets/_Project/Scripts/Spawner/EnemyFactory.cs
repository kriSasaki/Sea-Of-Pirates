using Project.Configs.Enemies;
using Project.Enemies;
using Project.Players.Logic;
using UnityEngine;

namespace Project.Spawner
{
    public class EnemyFactory
    {
        private readonly Enemy _prefab;
        private readonly VfxSpawner _vfxSpawner;
        private readonly Player _player;

        public EnemyFactory(Enemy prefab, VfxSpawner vfxSpawner, Player player)
        {
            _prefab = prefab;
            _vfxSpawner = vfxSpawner;
            _player = player;
        }

        public Enemy Create(EnemyConfig enemyConfig, Vector3 position, Transform parent = null)
        {
            Enemy enemy = Object.Instantiate(_prefab, position, Quaternion.identity, parent);
            enemy.Initialize(enemyConfig, _vfxSpawner, _player);

            return enemy;
        }
    }
}