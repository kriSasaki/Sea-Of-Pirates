using Project.Configs.GameResources;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StatConfig : ScriptableObject
{
    [SerializeField] private UpgradeCost _primaryCost;
    [SerializeField] private AdditionalUpgradeCost _secondaryCost;

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

    public bool IsLastLevel(int currentLevel)
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

public class StatSheet
{
    List<StatConfig> _stats;


}

public class PlayerStats
{
    public int Health;
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
