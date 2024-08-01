using Project.Interfaces.Stats;
using Project.Players.PlayerLogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerAttack _attackLeft;
    [SerializeField] private PlayerAttack _attackRight;
    [SerializeField] private PlayerHealth _health;
    [SerializeField] private PlayerMove _move;
    private IPlayerStats _playerStats;

    private int _maxHealth => _playerStats.MaxHealth;
    private int _damage => _playerStats.Damage;
    private int _speed => _playerStats.Speed;

    [Inject]
    public void Construct(IPlayerStats playerStats)
    {
        _playerStats = playerStats;
    }

    public void SetMaxHealth(int newMaxHealth)
    {
        newMaxHealth = _maxHealth;
    }

    public void SetDamage(int newDamage)
    {
        newDamage = _damage;
    }

    public void SetSpeed(int newSpeed)
    {
        newSpeed = _speed;
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }
}
