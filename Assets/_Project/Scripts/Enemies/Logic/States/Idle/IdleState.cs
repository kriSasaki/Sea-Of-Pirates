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
        }

        private void OnPlayerDetected()
        {
            StateMachine.SetState<BattleState>();
        }

        public override void Exit()
        {
            base.Exit();
            Detector.PlayerDetected -= OnPlayerDetected;
        }

        private async UniTaskVoid DetectPlayerAsync(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_playerDetectionDelay), cancellationToken: token);
            Detector.PlayerDetected += OnPlayerDetected;
            Detector.CheckPlayer();
        }
    }
}