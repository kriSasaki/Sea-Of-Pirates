using Project.Players.PlayerLogic;
using TMPro;
using UnityEngine;
using Zenject;

namespace Project.UI.Bars
{
    public class PlayerHealthBar : FadeableBar
    {
        [SerializeField] private TMP_Text _amountLabel;

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

            if (_player.CurrentHealth == _player.MaxHealth)
                TryFade(() => _player.CurrentHealth == _player.MaxHealth);
        }
    }
}