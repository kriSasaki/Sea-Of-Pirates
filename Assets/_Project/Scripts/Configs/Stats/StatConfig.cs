using System;
using System.Collections.Generic;
using System.Linq;
using Project.Configs.GameResources;
using Project.Interfaces.Data;
using UnityEngine;

public class StatConfig : ScriptableObject
{
    [SerializeField] private UpgradeCost _primaryCost;
    [SerializeField] private AdditionalUpgradeCost _secondaryCost;

    [field: SerializeField] public StatType StatType { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField, Min(0)] public int MinLevel { get; private set; }
    [field: SerializeField, Min(0)] public int MaxLevel { get; private set; }
    [field: SerializeField, Min(0)] public int MinValue { get; private set; }
    [field: SerializeField, Min(0)] public int MaxValue { get; private set; }

    public int GetValue(int currentLevel)
    {
        return (int)MathExtensions.Remap(currentLevel, MinLevel, MaxLevel, MinValue, MaxValue);
    }

    public List<GameResourceAmount> GetUpgradePrice(int currentLevel)
    {
        int nextLevel = currentLevel + 1;

        List<GameResourceAmount> price = new()
        {
            _primaryCost.GetCost(this, nextLevel)
        };

        if (_secondaryCost.IsApplicable(nextLevel))
        {
            price.Add(_secondaryCost.GetCost(this, nextLevel));
        }

        return price;
    }

    public bool IsMaxLevel(int currentLevel)
    {
        return currentLevel == MaxLevel;
    }
}

public static class MathExtensions
{
    public static float Remap(float source, float sourceFrom, float sourceTo, float targetFrom, float targetTo)
    {
        return targetFrom + (source - sourceFrom) * (targetTo - targetFrom) / (sourceTo - sourceFrom);
    }
}

[System.Serializable]
public class UpgradeCost
{
    [SerializeField] private GameResource _resource;

    [field: SerializeField] protected int MinCost { get; private set; }
    [field: SerializeField] protected int MaxCost { get; private set; }

    public GameResourceAmount GetCost(StatConfig stat, int level)
    {
        int cost = ComputeCost(stat, level);

        return new GameResourceAmount(_resource, cost);
    }

    protected virtual int ComputeCost(StatConfig statConfig, int level)
    {
        return (int)MathExtensions.Remap(level, statConfig.MinLevel, statConfig.MaxLevel, MinCost, MaxCost);
    }
}

[System.Serializable]
public class AdditionalUpgradeCost : UpgradeCost
{
    [SerializeField] private int _firstApplicationLevel;
    [SerializeField] private int _applicationInterval;

    public bool IsApplicable(int level)
    {
        return level >= _firstApplicationLevel && IsApplicableLevel(level);
    }

    protected override int ComputeCost(StatConfig statConfig, int level)
    {
        return (int)MathExtensions.Remap(level, _firstApplicationLevel, statConfig.MaxLevel, MinCost, MaxCost);
    }

    private bool IsApplicableLevel(int level)
    {
        return (level - _firstApplicationLevel) % _applicationInterval == 0;
    }
}

public struct GameResourceAmount
{
    public GameResource Resource;
    public int Amount;

    public GameResourceAmount(GameResource resource, int amount)
    {
        Resource = resource;
        Amount = amount;
    }
}

public class StatsSheet
{
    [SerializeField] private List<StatConfig> _stats;

    public StatConfig GetStatConfig(StatType statType)
    {
        return _stats.First(s => s.StatType == statType);
    }
}


[System.Serializable]
public class PlayerStatData
{
    public StatType StatType;
    public int Level;
}

public class PlayerStatsProvider : IPlayerStatsProvider
{
    private readonly IPlayerStatsData _statsData;
    private readonly StatsSheet _statsSheet;
    private readonly Dictionary<StatType, int> _statsLevels;

    public PlayerStatsProvider(IPlayerStatsData statsData, StatsSheet statsSheet)
    {
        _statsData = statsData;
        _statsSheet = statsSheet;
        _statsLevels = new();

        FillStatsLevels();
    }

    public Dictionary<StatType, PlayerStat> LoadStats()
    {
        Dictionary<StatType, PlayerStat> stats = new();

        foreach (StatType statType in _statsLevels.Keys)
        {
            stats.Add(statType, CreateStat(statType));
        }

        return stats;
    }

    public void UpdateStats(Dictionary<StatType, PlayerStat> stats)
    {
        foreach (StatType statType in stats.Keys)
        {
            PlayerStatData data = _statsData.StatsLevels.FirstOrDefault(s => s.StatType == statType);

            if (data != null)
            {
                data.Level = _statsLevels[statType];
            }
            else
            {
                _statsData.StatsLevels.Add(new PlayerStatData() { StatType = statType, Level = stats[statType].Level });
            }
        }

        _statsData.Save();
    }

    private void FillStatsLevels()
    {
        foreach (StatType statType in Enum.GetValues(typeof(StatType)).Cast<StatType>())
        {
            _statsLevels.Add(statType, 0);
        }
        foreach (PlayerStatData statData in _statsData.StatsLevels)
        {
            _statsLevels[statData.StatType] = statData.Level;
        }
    }

    private PlayerStat CreateStat(StatType type)
    {
        return new PlayerStat(_statsSheet.GetStatConfig(type), _statsLevels[type]);
    }
}

public class PlayerStat
{
    public PlayerStat(StatConfig config, int level)
    {
        Config = config;
        Level = level;
    }

    public StatConfig Config { get; }
    public int Level { get; private set; }

    public int GetValue()
    {
        return Config.GetValue(Level);
    }

    public void LevelUp()
    {
        Level++;
    }
}

public class PlayerStats : IPlayerStats
{
    private PlayerStatsProvider _provider;
    private Dictionary<StatType, PlayerStat> _stats;

    public int MaxHealth { get; private set; }
    public int Damage { get; private set; }
    public int CargoSize { get; private set; }
    public int Speed { get; private set; }
    public int AttackRange { get; private set; }
    public int CannonsAmount { get; private set; }

    public void Initialize(PlayerStatsProvider provider)
    {
        _provider = provider;
        _stats = _provider.LoadStats();

        UpdateStatsValues();
    }

    public void UpgradeStat(StatType type)
    {
        _stats[type].LevelUp();
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
        MaxHealth = GetStatValue(StatType.Health);
        Damage = GetStatValue(StatType.Damage);
        CargoSize = GetStatValue(StatType.CargoSize);
        CannonsAmount = GetStatValue(StatType.CannonsAmount);
        Speed = GetStatValue(StatType.Speed);
        AttackRange = GetStatValue(StatType.AttackRange);

        SaveStats();
    }

    private void SaveStats()
    {
        _provider.UpdateStats(_stats);
    }
}

[System.Serializable]
public enum StatType
{
    Health,
    Damage,
    CargoSize,
    Speed,
    AttackRange,
    CannonsAmount
}

public interface IPlayerStatsData : ISaveable
{
    List<PlayerStatData> StatsLevels { get; }
}

public interface IPlayerStats
{
    int MaxHealth { get; }
    int Damage { get; }
    int CargoSize { get; }
    int Speed { get; }
    int AttackRange { get; }
    int CannonsAmount { get; }
}

public interface IPlayerStatsProvider
{
    public Dictionary<StatType, PlayerStat> LoadStats();
    public void UpdateStats(Dictionary<StatType, PlayerStat> stats);
}
