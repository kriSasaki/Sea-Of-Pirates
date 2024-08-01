using Project.Interfaces.Stats;
using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace Project.Players.PlayerLogic
{
    public class PlayerHealth : MonoBehaviour
    {
        public event Action HealthChanged;

        [SerializeField] private Bars _bar;
        [SerializeField] private GameObject _hitEffect;
        [SerializeField] private AudioSource _soundOfHit;

        private float _effectTime = 0.2f;
        private Player _player;
        private int _currentHealth;
        private int _maxHealth;
        private float _minimalAudioPitch = 0.8f;
        private float _maximalAudioPitch = 1.2f;
        private IPlayerStats _playerStats;
        private bool _accessPirateBayAllowed = false;

        public bool AccessPirateBayAllowed => _accessPirateBayAllowed;

        public int CurrentHealth => _currentHealth;  

        private void Start()
        {
            _player = GetComponent<Player>();
            _player.SetMaxHealth(_maxHealth);
            _currentHealth = _maxHealth;

        }

        public void TakeDamage(int damage)
        {
            if (_currentHealth <= 0)
            {
                HealthChanged?.Invoke();
                return;
            }
            _soundOfHit.pitch = Random.Range(_minimalAudioPitch, _maximalAudioPitch);
            _hitEffect.SetActive(true);
            Invoke(nameof(HideFlash), _effectTime);
            _currentHealth -= damage;
            _bar.SetHealth(_currentHealth, _maxHealth);
        }

        private void HideFlash()
        {
            _hitEffect.SetActive(false);
        }
    }
}