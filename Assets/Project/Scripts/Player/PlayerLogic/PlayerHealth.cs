using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Scripts.Players.PlayerLogic.Health
{
    public class PlayerHealth : MonoBehaviour
    {
        private const string TurnOffFlash = "HideFlash";
        public Action HealthChange;

        [SerializeField] private Bars _bar;
        [SerializeField] private GameObject _hitEffect;
        [SerializeField] private AudioSource _soundOfHit;
        [SerializeField] private float _effectTime;

        private int _currentHealth;
        private int _maxHealth;
        private float _minimalAudioPitch = 0.8f;
        private float _maximalAudioPitch = 1.2f;

        public int CurrentHealth => _currentHealth;

        private void Awake()
        {
            _maxHealth = GetStatValue(StatType.Health);
        }

        private void Start()
        {
            _currentHealth = _maxHealth;
        }

        public int MaxHealth
        {
            get { return _maxHealth; }
            set { _maxHealth = value; }
        }

        public void TakeDamage(int damage)
        {
            if (_currentHealth <= 0)
            {
                HealthChange?.Invoke();
                return;
            }
            _soundOfHit.pitch = Random.Range(_minimalAudioPitch, _maximalAudioPitch);
            _hitEffect.SetActive(true);
            Invoke(TurnOffFlash, _effectTime);
            _currentHealth -= damage;
            _bar.SetHealth(_currentHealth, _maxHealth);
        }

        private void HideFlash()
        {
            _hitEffect.SetActive(false);
        }
    }
}