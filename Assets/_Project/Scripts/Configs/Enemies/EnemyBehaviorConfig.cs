using Project.Enemies.Logic.States;
using Project.Enemies.Logic.States.Battle;
using Project.Enemies.Logic.States.Idle;
using System;
using UnityEngine;

namespace Project.Configs.Enemies
{
    [CreateAssetMenu(fileName = "EnemyBehavior", menuName = "Configs/Enemies/Behaviour")]
    public class EnemyBehaviorConfig : ScriptableObject
    {
        [field: SerializeField] public IdleState IdleState { get; private set; }
        [field: SerializeField] public BattleState BattleState { get; private set; }
        [field: SerializeField] public DeadState DeadState { get; private set; }
    }
}
