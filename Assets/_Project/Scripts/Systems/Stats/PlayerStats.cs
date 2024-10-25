﻿using System;
using System.Collections.Generic;
using Scripts.Interfaces.Data;
using Scripts.Interfaces.Stats;

namespace Scripts.Systems.Stats
{
    public class PlayerStats : IPlayerStats, IUpgradableStats
    {
        private IPlayerStatsProvider _provider;
        private Dictionary<StatType, PlayerStat> _stats;

        public PlayerStats(IPlayerStatsProvider provider)
        {
            _provider = provider;
            _stats = _provider.LoadStats();

            SetStatValues();
        }

        public event Action StatsUpdated;

        public int MaxHealth { get; private set; }
        public int Damage { get; private set; }
        public int CargoSize { get; private set; }
        public int Speed { get; private set; }
        public int AttackRange { get; private set; }
        public int CannonsAmount { get; private set; }

        public void UpgradeStat(StatType type)
        {
            _stats[type].LevelUp();
            UpdateStatsValues();
        }

        public void SetStatValue(StatType type, int value)
        {
            _stats[type].SetLevel(value);
            UpdateStatsValues();
        }

        public int GetStatValue(StatType type)
        {
            return _stats[type].GetValue();
        }

        public int GetStatLevel(StatType type)
        {
            return _stats[type].Level;
        }

        private void UpdateStatsValues()
        {
            SetStatValues();
            SaveStats();
            StatsUpdated?.Invoke();
        }

        private void SetStatValues()
        {
            MaxHealth = GetStatValue(StatType.Health);
            Damage = GetStatValue(StatType.Damage);
            CargoSize = GetStatValue(StatType.CargoSize);
            CannonsAmount = GetStatValue(StatType.CannonsAmount);
            Speed = GetStatValue(StatType.Speed);
            AttackRange = GetStatValue(StatType.AttackRange);
        }

        private void SaveStats()
        {
            _provider.UpdateStats();
        }
    }
}