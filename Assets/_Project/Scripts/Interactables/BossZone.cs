using Scripts.Configs.Game;
using Scripts.Interfaces.Audio;
using Scripts.Players.Logic;
using Scripts.Spawner;
using UnityEngine;
using Zenject;

namespace Scripts.Interactables
{
    public class BossZone : CameraViewZone
    {
        [SerializeField] private BossSpawner _bossSpawner;

        private IAudioService _audioService;
        private GameConfig _gameConfig;

        protected override void OnPlayerEntered(Player player)
        {
            base.OnPlayerEntered(player);

            if (_bossSpawner.IsBossSpawned)
            {
                _audioService.PlaySound(_gameConfig.BossZoneSound);
                _audioService.PlayMusic(_gameConfig.BattleMusic);
            }
        }

        protected override void OnPlayerExited(Player player)
        {
            base.OnPlayerExited(player);

            _audioService.PlayMusic(_gameConfig.MainMusic);
        }

        [Inject]
        private void Construct(IAudioService audioService, GameConfig gameConfig)
        {
            _audioService = audioService;
            _gameConfig = gameConfig;
        }
    }
}