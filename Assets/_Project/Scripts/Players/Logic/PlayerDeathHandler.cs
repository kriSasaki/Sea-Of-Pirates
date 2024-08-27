using Cysharp.Threading.Tasks;
using Project.Interactables;
using Project.Interfaces.Audio;
using Project.Players.View;
using Project.UI;
using UnityEngine;
using Zenject;

namespace Project.Players.Logic
{
    public class PlayerDeathHandler : MonoBehaviour
    {
        [SerializeField] private AudioClip _deathSound;

        private Player _player;
        private PlayerView _playerView;
        private PirateBay _pirateBay;
        private PlayerDeathWindow _playerDeathWindow;
        private PlayerAttack _playerAttack;
        private IAudioService _audioService;

        [Inject]
        private void Construct(
            Player player,
            PlayerView playerView,
            PirateBay pirateBay,
            PlayerDeathWindow playerDeathWindow,
            PlayerAttack playerAttack,
            IAudioService audioService)
        {
            _player = player;
            _playerView = playerView;
            _pirateBay = pirateBay;
            _playerDeathWindow = playerDeathWindow;
            _playerAttack = playerAttack;
            _audioService = audioService;
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

            await _playerView.DieAsync();

            _playerDeathWindow.Show(RessurectPlayer);
        }

        private void RessurectPlayer()
        {
            _player.Heal();
            _player.transform.position = _pirateBay.PlayerRessurectPoint.position;
            _playerView.Ressurect();
            _playerAttack.enabled = true;
        }
    }
}