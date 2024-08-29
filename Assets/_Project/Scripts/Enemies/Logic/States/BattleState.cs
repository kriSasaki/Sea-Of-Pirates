namespace Project.Enemies.Logic.States
{
    public abstract class BattleState : AliveState
    {
        public override void Enter()
        {
            base.Enter();
            Detector.PlayerLost -= OnPlayerLost;

        }

        public override void Exit()
        {
            base.Exit();
            Detector.PlayerLost -= OnPlayerLost;
        }

        private void OnPlayerLost()
        {
            StateMachine.SetState(StateMachine._idleState);
        }
    }
}