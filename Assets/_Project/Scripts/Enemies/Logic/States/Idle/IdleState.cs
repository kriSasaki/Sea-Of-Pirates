using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Project.Enemies.Logic.States.Battle;
using UnityEngine;

namespace Project.Enemies.Logic.States.Idle
{
    public abstract class IdleState : AliveState
    {
        [SerializeField, Range(0f, 5f)] private float _playerDetectionDelay = 3f;

        public override void Enter()
        {
            base.Enter();
            DetectPlayerAsync(ExitToken).Forget();
            Enemy.Damaged += OnEnemyDamaged;
        }

        private void OnEnemyDamaged()
        {
            StateMachine.SetState<BattleState>();
        }

        private void OnPlayerDetected()
        {
            StateMachine.SetState<BattleState>();
        }

        public override void Exit()
        {
            base.Exit();
            Detector.PlayerDetected -= OnPlayerDetected;
            Enemy.Damaged -= OnEnemyDamaged;

        }

        private async UniTaskVoid DetectPlayerAsync(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_playerDetectionDelay), cancellationToken: token);
            Detector.PlayerDetected += OnPlayerDetected;
            Detector.CheckPlayer();
        }
    }
}