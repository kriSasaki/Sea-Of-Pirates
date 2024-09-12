using System;
using NaughtyAttributes;
using Project.Interactables;
using Project.Players.Logic;
using Project.Systems.Data;
using UnityEngine;
using Zenject;

namespace Project.Utils
{
    public class PlayerDebugCheats : MonoBehaviour
    {
        [SerializeField] private bool _setStatsOnStart = false;
        [HorizontalLine(3f, EColor.Orange)]
        [SerializeField, Range(1, 100)] private int _healthLevel;
        [SerializeField, Range(1, 100)] private int _damageLevel;
        [SerializeField, Range(1, 30)] private int _speedLevel;
        [SerializeField, Range(1, 100)] private int _cargoLevel;
        [SerializeField, Range(1, 50)] private int _attackRangeLevel;
        [SerializeField, Range(1, 10)] private int _cannonsLevel;

        private PlayerStats _playerStats;
        private Player _player;
        private PirateBay _pirateBay;

        private void Start()
        {
            if (_setStatsOnStart)
            {
                UpdateStats();
            }
        }

        [Inject]
        private void Construct(
            PlayerStats playerStats,
            Player player,
            PirateBay pirateBay)
        {
            _playerStats = playerStats;
            _player = player;
            _pirateBay = pirateBay;
        }

        [Button(enabledMode: EButtonEnableMode.Playmode)]
        private void UpdateStats()
        {
            _playerStats.SetStatValue(StatType.Health, _healthLevel);
            _playerStats.SetStatValue(StatType.Damage, _damageLevel);
            _playerStats.SetStatValue(StatType.Speed, _speedLevel);
            _playerStats.SetStatValue(StatType.CargoSize, _cargoLevel);
            _playerStats.SetStatValue(StatType.AttackRange, _attackRangeLevel);
            _playerStats.SetStatValue(StatType.CannonsAmount, _cannonsLevel);
        }

        [Button(enabledMode: EButtonEnableMode.Playmode)]
        private void MoveToPirateBay()
        {
            _player.SetPosition(_pirateBay.PlayerRessurectPoint.position);
        }
    }
}