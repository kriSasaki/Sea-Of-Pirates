using Scripts.Players.Logic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Scripts.UI.Bars
{
    public class PlayerHealthBar : FadeableBar
    {
        [SerializeField] private TMP_Text _amountLabel;
        [SerializeField] private Color _hitColor = Color.white;
        [SerializeField] private float _hitDuration = 0.15f;

        private Player _player;

        private void OnDestroy()
        {
            _player.HealthChanged -= OnHealthChanged;
        }

        [Inject]
        private void Construct(Player player)
        {
            _player = player;

            _player.HealthChanged += OnHealthChanged;
        }

        private void OnHealthChanged()
        {
            _amountLabel.text = _player.CurrentHealth.ToString();

            Fill(_player.CurrentHealth, _player.MaxHealth);
            LerpColor(_hitColor, _hitDuration);

            if (_player.CurrentHealth == _player.MaxHealth)
                TryFade(() => _player.CurrentHealth == _player.MaxHealth);
        }
    }
}