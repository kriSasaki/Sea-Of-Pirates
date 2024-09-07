using Project.Enemies.Logic.States.Idle;
using Project.Enemies.View;

namespace Project.Enemies.Logic.States.Battle
{
    public abstract class BattleState : AliveState
    {
        private AttackRangeView _attackRangeView;
        private EnemyView _enemyView;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            _attackRangeView = Enemy.AttackRangeView;
            _enemyView = Enemy.View;
        }

        public override void Enter()
        {
            base.Enter();
            Detector.PlayerLost += OnPlayerLost;
            Player.Died += OnPlayerDied;
            _attackRangeView.ShowAttackRange();
            _enemyView.ShowHud();
        }

        public override void Exit()
        {
            base.Exit();
            Detector.PlayerLost -= OnPlayerLost;
            Player.Died -= OnPlayerDied;
            _attackRangeView.HideAttackRange();
            _enemyView.HideHud();
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