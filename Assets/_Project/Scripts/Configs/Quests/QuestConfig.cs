using Project.Configs.GameResources;
using Project.Enemies;
using Project.Systems.Stats;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Configs.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Configs/Quest")]
    public class QuestConfig : ScriptableObject
    {
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public EnemyConfig TargetType { get; private set; }
        [field: SerializeField, Min(1)] public int TargetAmount { get; private set; }
        [field: SerializeField] public GameResourceAmount Reward { get; private set; }
    }
}