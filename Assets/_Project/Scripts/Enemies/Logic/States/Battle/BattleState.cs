using Project.Enemies.Logic.States.Idle;

namespace Project.Enemies.Logic.States.Battle
{
    public abstract class BattleState : AliveState
    {
        public override void Enter()
        {
            base.Enter();
            Detector.PlayerLost += OnPlayerLost;
            Player.Died += OnPlayerDied;

        }

        public override void Exit()
        {
            base.Exit();
            Detector.PlayerLost -= OnPlayerLost;
            Player.Died -= OnPlayerDied;
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