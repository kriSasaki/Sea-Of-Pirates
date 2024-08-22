using Project.Interfaces.Audio;
using Project.Interfaces.Stats;
using Project.Players.PlayerLogic;
using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    [SerializeField] private GameObject _hitEffect;
    [SerializeField] private AudioClip _audioClip;

    private IPlayerStats _playerStats;
    private IAudioService _audioService;

    private float _effectTime = 0.2f;
    private int _currentHealth;

    public event Action HealthChanged;

    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _playerStats.MaxHealth;

    private void Start()
    {
        HealthChanged?.Invoke();
    }

    [Inject]
    public void Construct(IPlayerStats playerStats, IAudioService audioService)
    {
        _playerStats = playerStats;
        _audioService = audioService;

        _currentHealth = MaxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth = Math.Max(_currentHealth - damage, 0);

        HealthChanged?.Invoke();
        ShowHitEffect();
    }

    private void ShowHitEffect()
    {
        _audioService.PlaySound(_audioClip);
        _hitEffect.SetActive(true);
        Invoke(nameof(HideFlash), _effectTime);
    }

    private void HideFlash()
    {
        _hitEffect.SetActive(false);
    }
}
