using UnityEngine;

namespace Scripts.Enemies.Logic.States.Battle
{
    [CreateAssetMenu(fileName = "NeutralBattle", menuName = "Configs/Enemies/States/NeutralBattle")]

    public class NeutralBattle : BattleState
    {
        [SerializeField] private AttackBattle _attackState;

        public override void Enter()
        {
            base.Enter();

            Enemy.Damaged += OnEnemyDamaged;
        }

        public override void Exit()
        {
            base.Exit();

            Enemy.Damaged -= OnEnemyDamaged;
        }

        public override void Update()
        {
            base.Update();

            Enemy.Mover.RotateTowards(Player.Position);
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            StateMachine.RegisterState<AttackBattle>(_attackState);
        }

        private void OnEnemyDamaged()
        {
            StateMachine.SetState<AttackBattle>();
        }
    }
}