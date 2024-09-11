using System;
using Project.Systems.Data;
using UnityEngine;
using Zenject;

namespace Project.Utils
{
    public class PlayerDebugCheats : MonoBehaviour
    {
        [SerializeField, Range(1, 100)] private int _healthLevel;
        [SerializeField, Range(1, 100)] private int _damageLevel;
        [SerializeField, Range(1, 30)] private int _speedLevel;
        [SerializeField, Range(1, 100)] private int _cargoLevel;
        [SerializeField, Range(1, 50)] private int _attackRangeLevel;
        [SerializeField, Range(1, 10)] private int _cannonsLevel;

        private PlayerStats _playerStats;

        private void Start()
        {
            _playerStats.SetStatValue(StatType.Health, _healthLevel);
            _playerStats.SetStatValue(StatType.Damage, _damageLevel);
            _playerStats.SetStatValue(StatType.Speed, _speedLevel);
            _playerStats.SetStatValue(StatType.CargoSize, _cargoLevel);
            _playerStats.SetStatValue(StatType.AttackRange, _attackRangeLevel);
            _playerStats.SetStatValue(StatType.CannonsAmount, _cannonsLevel);
        }

        [Inject]
        public void Construct(PlayerStats playerStats)
        {
            _playerStats = playerStats;
        }
    }
}