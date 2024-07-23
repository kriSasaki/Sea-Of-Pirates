using Project.Interfaces.Stats;
using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Scripts.Players.PlayerLogic.Health
{
    public class PlayerHealth : MonoBehaviour
    {
        private const string TurnOffFlash = "HideFlash";

        [SerializeField] private Bars _bar;
        [SerializeField] private GameObject _hitEffect;
        [SerializeField] private AudioSource _soundOfHit;
        [SerializeField] private float _effectTime;

        private int _currentHealth;
        private float _minimalAudioPitch = 0.8f;
        private float _maximalAudioPitch = 1.2f;
        private IPlayerStats _playerStats;

        public Action HealthChange;

        public int CurrentHealth => _currentHealth;

        private int MaxHealth => _playerStats.MaxHealth;

        private void Start()
        {
            _currentHealth = MaxHealth;
        }

        [Inject]
        public void Construct(IPlayerStats playerStats)
        {
            _playerStats = playerStats;
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
            _bar.SetHealth(_currentHealth, MaxHealth);
        }

        private void HideFlash()
        {
            _hitEffect.SetActive(false);
        }
    }
}