using Project.Enemies.Logic;
using Project.UI.Bars;
using Project.Utils.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.Enemies.View
{
    public class EnemyHudView : MonoBehaviour
    {
        private Enemy _enemy;

        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _lootIcon;
        [SerializeField] private TMP_Text _lootAmount;
        [SerializeField] private FillableBar _healthBar;
        [SerializeField] private Color _hitColor = Color.white;
        [SerializeField] private float _hitDuration = 0.15f;

        public void Initialize(Enemy enemy)
        {
            _enemy = enemy;
            var loot = _enemy.Loot;
            _lootIcon.sprite = loot.Resource.Sprite;
            _lootAmount.text = loot.Amount.ToNumericalString();

            _enemy.HealthChanged += OnHealthChanged;
            Hide();
        }

        private void OnDestroy()
        {
            _enemy.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int currentHealth, int maxHealth)
        {
            _healthBar.Fill(currentHealth, maxHealth);
            _healthBar.LerpColor(_hitColor, _hitDuration);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}