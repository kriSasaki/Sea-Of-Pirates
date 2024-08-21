using Project.Interfaces.Audio;
using Project.Interfaces.Stats;
using Project.Players.PlayerLogic;
using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

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
    public int MaxHealth => _playerStats.MaxHealth;

    [Inject]
    public void Construct(IPlayerStats playerStats, IAudioService audioService)
    {
        _playerStats = playerStats;
        _audioService = audioService;

        _currentHealth = MaxHealth;
    }

    private void Start()
    {
        if (_playerStats != null)
        {
            _currentHealth = MaxHealth;
            _bar.SetHealth(_currentHealth, MaxHealth);
        }

        HealthChanged?.Invoke();
    }

    public void TakeDamage(int damage)
    {
        HealthChanged?.Invoke();

        if (_currentHealth <= 0)
        {
            return;
        }

        _hitEffect.SetActive(true);
        Invoke(nameof(HideFlash), _effectTime);
        _audioService.PlaySound(_audioClip);
        _currentHealth -= damage;
        _bar.SetHealth(_currentHealth, MaxHealth);
    }

    private void HideFlash()
    {
        _hitEffect.SetActive(false);
    }
}
