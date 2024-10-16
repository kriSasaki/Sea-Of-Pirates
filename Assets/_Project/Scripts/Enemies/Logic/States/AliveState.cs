using System.Threading;
using Scripts.Interfaces.Enemies;

namespace Scripts.Enemies.Logic.States
{
    public abstract class AliveState : BaseState
    {
        private CancellationTokenSource _exitCancellation;

        protected CancellationToken ExitToken => _exitCancellation.Token;
        protected PlayerDetector Detector => Enemy.Detector;

        private void OnDestroy()
        {
            _exitCancellation?.Cancel();
            _exitCancellation?.Dispose();
        }

        public override void Enter()
        {
            base.Enter();

            _exitCancellation?.Dispose();
            _exitCancellation = new();

            Enemy.Died += OnEnemyDied;
            Detector.Enable();
        }

        public override void Exit()
        {
            base.Exit();

            _exitCancellation.Cancel();

            Enemy.Died -= OnEnemyDied;
        }

        private void OnEnemyDied(IEnemy enemy)
        {
            StateMachine.SetState<DeadState>();
        }
    }
}