using Ami.BroAudio;
using Project.Configs.Game;
using Project.Interfaces.Audio;
using Project.Players.Logic;
using Project.Spawner;
using UnityEngine;
using Zenject;

namespace Project.Interactables
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
        public void Construct(IAudioService audioService, GameConfig gameConfig)
        {
            _audioService = audioService;
            _gameConfig = gameConfig;
        }
    }
}