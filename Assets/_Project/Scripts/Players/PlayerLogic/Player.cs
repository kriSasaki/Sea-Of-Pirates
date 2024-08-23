using Project.Interfaces.Audio;
using Project.Interfaces.Hold;
using Project.Interfaces.Stats;
using Project.Players.PlayerLogic;
using Project.Utils.VFX;
using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip _audioClip;

    private IPlayerStats _playerStats;
    private IAudioService _audioService;
    private IPlayerHold _playerHold;
    private VfxSpawner _vfxSpawner;

    private int _currentHealth;

    public event Action HealthChanged;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _playerStats.MaxHealth;

    private void Start()
    {
        HealthChanged?.Invoke();
    }

    [Inject]
    public void Construct(
        IPlayerStats playerStats,
        IAudioService audioService,
        IPlayerHold playerHold,
        VfxSpawner vfxSpawner)
    {
        _playerStats = playerStats;
        _audioService = audioService;
        _playerHold = playerHold;
        _vfxSpawner = vfxSpawner;
        _currentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth = Math.Max(_currentHealth - damage, 0);

        HealthChanged?.Invoke();
        ShowHitEffect();
    }

    public void RestoreHealthMaximum()
    {
        _currentHealth = MaxHealth;
        HealthChanged?.Invoke();
    }

    public void LoadPlayerHoldStorage()
    {
        _playerHold.LoadToStorage();
    }

    private void ShowHitEffect()
    {
        _audioService.PlaySound(_audioClip);
        _vfxSpawner.SpawnExplosion(transform.position);
    }
}
