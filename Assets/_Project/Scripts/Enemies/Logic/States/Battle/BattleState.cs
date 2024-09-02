using Project.Enemies.Logic.States.Idle;
using Project.Enemies.View;
using UnityEditor.Build;

namespace Project.Enemies.Logic.States.Battle
{
    public abstract class BattleState : AliveState
    {
        private AttackRangeView _attackRangeView;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            _attackRangeView = Enemy.AttackRangeView;
        }

        public override void Enter()
        {
            base.Enter();
            Detector.PlayerLost += OnPlayerLost;
            Player.Died += OnPlayerDied;
            _attackRangeView.ShowAttackRange();

        }

        public override void Exit()
        {
            base.Exit();
            Detector.PlayerLost -= OnPlayerLost;
            Player.Died -= OnPlayerDied;
            _attackRangeView.HideAttackRange();
        }

        private void OnPlayerLost()
        {
            StateMachine.SetState<IdleState>();
        }

        private void OnPlayerDied()
        {
            StateMachine.SetState<IdleState>();
        }
    }
}