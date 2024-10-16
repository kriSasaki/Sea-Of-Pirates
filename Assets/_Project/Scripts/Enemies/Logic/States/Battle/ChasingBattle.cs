using Scripts.Enemies.Logic.States.Idle;
using UnityEngine;

namespace Scripts.Enemies.Logic.States.Battle
{
    [CreateAssetMenu(fileName = "ChasingBattle", menuName = "Configs/Enemies/States/ChasingBattle")]
    public class ChasingBattle : BattleState
    {
        [SerializeField] private AttackBattle _attackState;
        [SerializeField] private float _maxDistanceFromSpawn;

        private float AttackRange => Config.AttackRange;

        public override void Update()
        {
            base.Update();

            Enemy.Mover.Move(Player.Position);

            float distanceFromSpawn = Vector3.Distance(Enemy.SpawnPosition, Enemy.Position);

            if (distanceFromSpawn >= _maxDistanceFromSpawn)
            {
                StateMachine.SetState<IdleState>();

                return;
            }

            float distanceToTarget = Vector3.Distance(Player.Position, Enemy.Position);

            if (distanceToTarget <= AttackRange)
            {
                StateMachine.SetState<AttackBattle>();
            }
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            StateMachine.RegisterState(_attackState);
        }
    }
}