using Project.Interfaces.Hold;
using Project.Interfaces.Stats;
using Project.Players.View;
using System;
using UnityEngine;
using Zenject;

namespace Project.Players.Logic
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerView _view;

        private IPlayerStats _playerStats;
        private IPlayerHold _playerHold;

        private int _currentHealth;

        public event Action HealthChanged;
        public event Action Died;

        public Vector3 Position => transform.position;
        public int CurrentHealth => _currentHealth;
        public int MaxHealth => _playerStats.MaxHealth;
        public bool IsAlive => _currentHealth > 0;

        private void Start()
        {
            HealthChanged?.Invoke();
        }

        [Inject]
        public void Construct(IPlayerStats playerStats, IPlayerHold playerHold)
        {
            _playerStats = playerStats;
            _playerHold = playerHold;
            _currentHealth = MaxHealth;
        }

        public void TakeDamage(int damage)
        {
            if (IsAlive == false)
                return;

            _currentHealth = Math.Max(_currentHealth - damage, 0);

            HealthChanged?.Invoke();
            _view.TakeDamage(damage);

            if (IsAlive == false)
                Died?.Invoke();
        }

        public void Heal()
        {
            _currentHealth = MaxHealth;
            HealthChanged?.Invoke();
        }

        public void UnloadHold()
        {
            _playerHold.LoadToStorage();
        }
    }
}