using Ami.BroAudio;
using Cysharp.Threading.Tasks;
using Scripts.Interactables;
using Scripts.Interfaces.Audio;
using Scripts.Interfaces.SDK;
using Scripts.Players.View;
using Scripts.SDK.Advertisment;
using Scripts.UI;
using UnityEngine;
using Zenject;

namespace Scripts.Players.Logic
{
    public class PlayerDeathHandler : MonoBehaviour
    {
        [SerializeField] private SoundID _deathSound;

        private Player _player;
        private PlayerView _playerView;
        private PirateBay _pirateBay;
        private PlayerDeathWindow _playerDeathWindow;
        private PlayerAttack _playerAttack;
        private IAudioService _audioService;
        private IMetricaService _metricaService;
        private AdvertismentController _advertisingController;

        [Inject]
        private void Construct(
            Player player,
            PlayerView playerView,
            PirateBay pirateBay,
            PlayerDeathWindow playerDeathWindow,
            PlayerAttack playerAttack,
            AdvertismentController advertismentController,
            IMetricaService metricaService,
            IAudioService audioService)
        {
            _player = player;
            _playerView = playerView;
            _pirateBay = pirateBay;
            _playerDeathWindow = playerDeathWindow;
            _playerAttack = playerAttack;
            _advertisingController = advertismentController;
            _metricaService = metricaService;
            _audioService = audioService;

            _player.Died += OnPlayerDied;
        }

        private void OnDestroy()
            => _player.Died -= OnPlayerDied;

        private void OnPlayerDied()
        {
            Die().Forget();
            _metricaService.SendPlayerDieEvent();
        }

        private async UniTaskVoid Die()
        {
            _playerAttack.enabled = false;
            _audioService.PlaySound(_deathSound);

            await _playerView.DieAsync();

            _playerDeathWindow.Show(RessurectPlayer);
        }

        private void RessurectPlayer()
        {
            _advertisingController.ShowIntersticialAd();
            _player.SetPosition(_pirateBay.PlayerRessurectPoint.position);

            _player.Heal();
            _playerView.Ressurect();
            _playerAttack.enabled = true;
        }
    }
}