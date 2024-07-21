using System;
using System.Collections.Generic;
using Project.Configs.GameResources;
using Project.Interfaces.Data;
using UnityEngine;
using System.Linq;
using UnityEngine.PlayerLoop;
using Project.Systems.Data;
using UnityEditor.SceneManagement;

public class StatConfig : ScriptableObject
{
    [SerializeField] private UpgradeCost _primaryCost;
    [SerializeField] private AdditionalUpgradeCost _secondaryCost;

    [field: SerializeField] public StatType StatType { get; private set; }
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public int MinLevel { get; private set; }
    [field: SerializeField] public int MaxLevel { get; private set; }
    [field: SerializeField] public int MinValue { get; private set; }
    [field: SerializeField] public int MaxValue { get; private set; }

    public int GetValue(int currentLevel)
    {
        return (int)MathExtensions.Remap(currentLevel, MinLevel, MaxLevel, MinValue, MaxValue);
    }

    public List<GameResourceAmount> GetUpgradePrice(int currentLevel)
    {
        int nextLevel = currentLevel + 1;
        List<GameResourceAmount> price = new List<GameResourceAmount>();

        price.Add(_primaryCost.GetCost(this, nextLevel));

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
    [SerializeField] private GameResource _gameResource;
    [field: SerializeField] protected int MinCost { get; private set; }
    [field: SerializeField] protected int MaxCost { get; private set; }

    public GameResourceAmount GetCost(StatConfig stat, int level)
    {
        int cost = ComputeCost(stat, level);

        return new GameResourceAmount(_gameResource, cost);
    }

    protected virtual int ComputeCost(StatConfig stat, int level)
    {
        return (int)MathExtensions.Remap(level, stat.MinLevel, stat.MaxLevel, MinCost, MaxCost);
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

    protected override int ComputeCost(StatConfig stat, int level)
    {
        return (int)MathExtensions.Remap(level, _firstApplicationLevel, stat.MaxLevel, MinCost, MaxCost);
    }

    private bool IsApplicableLevel(int level)
    {
        return (level - _firstApplicationLevel) % _applicationInterval == 0;
    }
}

public struct GameResourceAmount
{
    public GameResource Type;
    public int Amount;

    public GameResourceAmount(GameResource type, int amount)
    {
        Type = type;
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
public class StatData
{
    public StatType StatType;
    public int Level;
}

public class PlayerStatsProvider
{
    private IPlayerStatsData _statsData;
    private StatsSheet _statsSheet;
    private Dictionary<StatType, int> _statsLevels;

    private Dictionary<StatConfig, int> _stats;

    public PlayerStatsProvider(IPlayerStatsData statsData, StatsSheet statsSheet)
    {
        _statsData = statsData;
        _statsSheet = statsSheet;

        _statsLevels = new();

        foreach (StatType statType in Enum.GetValues(typeof(StatType)).Cast<StatType>())
        {
            _statsLevels.Add(statType, 0);
        }
        foreach (StatData statData in _statsData.StatsLevels)
        {
            _statsLevels[statData.StatType] = statData.Level;
        }
    }
}

public class PlayerStats
{
    private Dictionary<StatType, int> _statsLevels;
    private Dictionary<StatConfig, int> _stats;
    private StatsSheet _statSheet;

    private IPlayerStatsData _statsData;
    public void Initialize(IPlayerStatsData statsData)
    {
        _statsData = statsData;

        foreach(StatType statType in Enum.GetValues(typeof(StatType)).Cast<StatType>())
        {
            _statsLevels.Add(statType, 0);
        }

        foreach(StatData statData in _statsData.StatsLevels)
        {
            _statsLevels[statData.StatType] = statData.Level;
        }
    }

    private void UpdateStatsValues()
    {
        MaxHealth = GetStatValue(StatType.Health);
        Damage = GetStatValue(StatType.Damage);
        CargoSize = GetStatValue(StatType.CargoSize);
        CannonsAmount = GetStatValue(StatType.CannonsAmount);

    }

    public int GetStatValue(StatType statType)
    {
        return _statSheet.GetStatConfig(StatType.Health).GetValue(_statsLevels[StatType.Health]);
    }

    private void SaveStats()
    {
        foreach (StatType statType in _statsLevels.Keys)
        {
            StatData data = _statsData.StatsLevels.FirstOrDefault(s => s.StatType == statType);

            if (data != null)
            {
                data.Level = _statsLevels[statType];
            }
            else
            {
                _statsData.StatsLevels.Add(new StatData() { StatType = statType, Level = _statsLevels[statType] });
            }
        }

        _statsData.Save();
    }

    public int MaxHealth;
    public int Damage;
    public int CargoSize;
    public float Speed;
    public float AttackRange;
    public int CannonsAmount;
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

public interface IPlayerStatsData: ISaveable
{
    List<StatData> StatsLevels { get; }
}
