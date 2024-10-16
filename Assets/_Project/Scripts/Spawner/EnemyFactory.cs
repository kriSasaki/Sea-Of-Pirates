using Scripts.Configs.Enemies;
using Scripts.Configs.Level;
using Scripts.Enemies.Logic;
using Scripts.Interfaces.Audio;
using Scripts.Players.Logic;
using UnityEngine;

namespace Scripts.Spawner
{
    public class EnemyFactory
    {
        private readonly Enemy _prefab;
        private readonly VfxSpawner _vfxSpawner;
        private readonly Player _player;
        private readonly IAudioService _audioService;
        private readonly LevelConfig _levelConfig;

        public EnemyFactory(
            Enemy prefab,
            VfxSpawner vfxSpawner,
            Player player,
            IAudioService audioService,
            LevelConfig levelConfig)
        {
            _prefab = prefab;
            _vfxSpawner = vfxSpawner;
            _player = player;
            _audioService = audioService;
            _levelConfig = levelConfig;
        }

        public Enemy Create(EnemyConfig enemyConfig, Vector3 position, Transform parent = null)
        {
            Enemy enemy = Object.Instantiate(_prefab, position, Quaternion.identity, parent);
            enemy.name = enemyConfig.name;
            enemy.Initialize(enemyConfig, _vfxSpawner, _player, _audioService, _levelConfig);

            return enemy;
        }
    }
}