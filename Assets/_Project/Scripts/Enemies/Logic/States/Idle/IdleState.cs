using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using Scripts.Enemies.Logic.States.Battle;
using UnityEngine;

namespace Scripts.Enemies.Logic.States.Idle
{
    public abstract class IdleState : AliveState
    {
        [SerializeField, Range(0f, 5f)] private float _playerDetectionDelay = 3f;
        [SerializeField] private bool _isHealState;
        [SerializeField, ShowIf(nameof(_isHealState))] private float _healDelay = 5f;

        public override void Enter()
        {
            base.Enter();

            DetectPlayerAsync(ExitToken).Forget();

            if (_isHealState)
                HealAsync(ExitToken).Forget();

            Enemy.Damaged += OnEnemyDamaged;
        }

        public override void Exit()
        {
            base.Exit();

            Detector.PlayerDetected -= OnPlayerDetected;
            Enemy.Damaged -= OnEnemyDamaged;
        }

        private void OnEnemyDamaged()
        {
            StateMachine.SetState<BattleState>();
        }

        private void OnPlayerDetected()
        {
            if (Player.IsAlive)
                StateMachine.SetState<BattleState>();
        }

        private async UniTaskVoid DetectPlayerAsync(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_playerDetectionDelay), cancellationToken: token);
            Detector.PlayerDetected += OnPlayerDetected;
            Detector.CheckPlayer();
        }

        private async UniTaskVoid HealAsync(CancellationToken token)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_healDelay), cancellationToken: token);
            Enemy.RestoreHealth();
        }
    }
}