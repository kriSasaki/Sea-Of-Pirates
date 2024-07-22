﻿using System.Collections.Generic;
using System.Linq;
using Project.Systems.Stats;
using UnityEngine;

[CreateAssetMenu(fileName = "StatsSheet", menuName = "Configs/Stats/StatSheet", order = 1)]
public class StatsSheet : ScriptableObject
{
    [SerializeField] private List<StatConfig> _stats;

    public IEnumerable<StatConfig> Stats => _stats;

    public StatConfig GetStatConfig(StatType statType)
    {
        return _stats.First(s => s.StatType == statType);
    }
}