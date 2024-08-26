using Cysharp.Threading.Tasks;
using Project.Interactables;
using Project.Interfaces.Audio;
using Project.UI;
using Project.Utils.Tweens;
using UnityEngine;
using Zenject;

namespace Project.Players.PlayerLogic
{
    public class PlayerDeathHandler : MonoBehaviour
    {
        [SerializeField] private AudioClip _deathSound;
        [SerializeField] private SinkTween _sinkTween;

        private Player _player;
        private Transform _shipTransform;
        private PirateBay _pirateBay;
        private PlayerDeathWindow _playerDeathWindow;
        private PlayerAttack _playerAttack;
        private IAudioService _audioService;

        private void Awake()
        {
            _sinkTween.Initialize(_shipTransform);
        }

        [Inject]
        private void Construct(
            Player player,
            PirateBay pirateBay,
            PlayerDeathWindow playerDeathWindow,
            PlayerAttack playerAttack,
            IAudioService audioService)
        {
            _player = player;
            _pirateBay = pirateBay;
            _playerDeathWindow = playerDeathWindow;
            _playerAttack = playerAttack;
            _audioService = audioService;
            _shipTransform = _player.transform;

            _player.HealthChanged += OnHealthChanged;
        }


        private void OnDestroy() => _player.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (!_player.IsAlive)
            {
                Die().Forget();
            }
        }

        private async UniTaskVoid Die()
        {
            _playerAttack.enabled = false;
            _audioService.PlaySound(_deathSound);

            await _sinkTween.Sink();

            _playerDeathWindow.Show(RessurectPlayer);
        }

        private void RessurectPlayer()
        {
            _player.Heal();

            _shipTransform.position = _pirateBay.PlayerRessurectPoint.position;
            _shipTransform.rotation = Quaternion.identity;
            _playerAttack.enabled = true;
        }
    }
}