using Scripts.Enemies.Logic.States;
using Scripts.Enemies.Logic.States.Battle;
using Scripts.Enemies.Logic.States.Idle;
using UnityEngine;

namespace Scripts.Configs.Enemies
{
    [CreateAssetMenu(fileName = "EnemyBehavior", menuName = "Configs/Enemies/Behaviour")]
    public class EnemyBehaviorConfig : ScriptableObject
    {
        [field: SerializeField] public IdleState IdleState { get; private set; }
        [field: SerializeField] public BattleState BattleState { get; private set; }
        [field: SerializeField] public DeadState DeadState { get; private set; }
    }
}
