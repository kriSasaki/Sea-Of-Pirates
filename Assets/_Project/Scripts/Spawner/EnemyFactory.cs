using Project.Configs.Enemies;
using Project.Enemies.Logic;
using Project.Enemies.View;
using Project.Interfaces.Audio;
using Project.Players.Logic;
using UnityEngine;

namespace Project.Spawner
{
    public class EnemyFactory
    {
        private readonly Enemy _prefab;
        private readonly VfxSpawner _vfxSpawner;
        private readonly Player _player;
        private readonly IAudioService _audioService;

        public EnemyFactory(Enemy prefab, VfxSpawner vfxSpawner, Player player, IAudioService audioService)
        {
            _prefab = prefab;
            _vfxSpawner = vfxSpawner;
            _player = player;
            _audioService = audioService;
        }

        public Enemy Create(EnemyConfig enemyConfig, Vector3 position, Transform parent = null)
        {
            Enemy enemy = Object.Instantiate(_prefab, position, Quaternion.identity, parent);
            enemy.name = enemyConfig.name;
            enemy.Initialize(enemyConfig, _vfxSpawner, _player, _audioService);

            return enemy;
        }
    }
}