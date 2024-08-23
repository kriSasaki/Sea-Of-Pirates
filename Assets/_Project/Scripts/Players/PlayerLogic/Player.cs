using Project.Interfaces.Audio;
using Project.Interfaces.Stats;
using Project.Players.PlayerLogic;
using System;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    public event Action HealthChanged;

    [SerializeField] private Bars _bar;
    [SerializeField] private GameObject _hitEffect;
    [SerializeField] private AudioClip _audioClip;

    private float _effectTime = 0.2f;
    private Player _player;
    private int _currentHealth;
    private IPlayerStats _playerStats;
    private IAudioService _audioService;

    public int CurrentHealth => _currentHealth;
    private int _maxHealth => _playerStats.MaxHealth;

    [Inject]
    public void Construct(IPlayerStats playerStats, IAudioService audioService)
    {
        _playerStats = playerStats;
        _audioService = audioService;
    }

    private void Start()
    {
        if (_playerStats != null)
        {
            _currentHealth = _maxHealth;
            _bar.SetHealth(_currentHealth, _maxHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth <= 0)
        {
            HealthChanged?.Invoke();
            return;
        }
        
        _hitEffect.SetActive(true);
        Invoke(nameof(HideFlash), _effectTime);
        _audioService.PlaySound(_audioClip);
        _currentHealth -= damage;
        _bar.SetHealth(_currentHealth, _maxHealth);
    }

    private void HideFlash()
    {
        _hitEffect.SetActive(false);
    }
}
