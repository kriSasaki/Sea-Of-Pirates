using Scripts.Enemies.Logic;
using Scripts.Systems.Data;
using Scripts.UI.Bars;
using Scripts.Utils.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Enemies.View
{
    public class EnemyHudView : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private Image _lootIcon;
        [SerializeField] private TMP_Text _lootAmount;
        [SerializeField] private FillableBar _healthBar;
        [SerializeField] private Color _hitColor = Color.white;
        [SerializeField] private float _hitDuration = 0.15f;

        private Enemy _enemy;

        private void OnDestroy()
        {
            _enemy.HealthChanged -= OnHealthChanged;
        }

        public void Initialize(Enemy enemy)
        {
            _enemy = enemy;
            GameResourceAmount loot = _enemy.Loot;

            _lootIcon.sprite = loot.Resource.Sprite;
            _lootAmount.text = loot.Amount.ToNumericalString();

            _enemy.HealthChanged += OnHealthChanged;
            Hide();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void OnHealthChanged(int currentHealth, int maxHealth)
        {
            _healthBar.Fill(currentHealth, maxHealth);
            _healthBar.LerpColor(_hitColor, _hitDuration);
        }
    }
}