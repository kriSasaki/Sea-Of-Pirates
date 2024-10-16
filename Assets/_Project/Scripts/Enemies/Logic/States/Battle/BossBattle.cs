using System;
using System.Collections.Generic;
using System.Threading;
using Ami.BroAudio;
using Cysharp.Threading.Tasks;
using NaughtyAttributes;
using Scripts.Utils.Extensions;
using UnityEngine;

namespace Scripts.Enemies.Logic.States.Battle
{
    [CreateAssetMenu(fileName = "BossBattle", menuName = "Configs/Enemies/States/BossBattle")]

    public class BossBattle : BattleState
    {
        private const float LaunchDelay = 0.05f;

        [SerializeField] private float _attackAngle = 50;
        [SerializeField] private float _attackPhaseDuration = 10f;
        [SerializeField] private float _phaseSwitchDuration = 2f;
        [HorizontalLine(3f, EColor.Blue)]
        [SerializeField] private float _volleyPhaseDuration = 10f;
        [SerializeField] private float _volleyRange = 15f;
        [SerializeField] private float _volleyInterval = 2f;
        [HorizontalLine(3f, EColor.Blue)]
        [SerializeField] private float _projectileRadius = 4f;
        [SerializeField] private float _projectileExplodeDelay = 4f;
        [SerializeField] private int _projectilesAmount = 10;
        [SerializeField] private SoundID _shootSound;

        private float HalfAttackAngle => _attackAngle / 2f;

        public override void Enter()
        {
            base.Enter();

            EnterAttackPhase().Forget();
        }

        public override void Exit()
        {
            base.Exit();

            Enemy.AttackRangeView.HideAttackRange();
        }

        private async UniTaskVoid EnterAttackPhase()
        {
            Enemy.AttackRangeView.ShowAttackRange();

            await UniTask.Delay(TimeSpan.FromSeconds(_phaseSwitchDuration), cancellationToken: ExitToken);

            using var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(ExitToken);

            TimeSpan phaseDuration = TimeSpan.FromSeconds(_attackPhaseDuration);
            UniTask phaseEndTask = UniTask.Delay(phaseDuration, cancellationToken: linkedCts.Token);
            UniTask playerInZoneTask = UniTask.WaitUntil(() => CanAttackPlayer(), cancellationToken: linkedCts.Token);

            UniTask<int>.Awaiter conditonAwaiter = UniTask.WhenAny(phaseEndTask, playerInZoneTask).GetAwaiter();

            while (conditonAwaiter.IsCompleted == false)
            {
                Enemy.Mover.RotateTowards(Player.Position, GetClosestSide());

                await UniTask.NextFrame();
            }

            linkedCts.Cancel();

            if (CanAttackPlayer())
            {
                Enemy.DealDamageAsync(Player).Forget();
            }

            EnterVolleyPhase().Forget();
        }

        private async UniTaskVoid EnterVolleyPhase()
        {
            Enemy.AttackRangeView.HideAttackRange();

            await UniTask.Delay(TimeSpan.FromSeconds(_phaseSwitchDuration), cancellationToken: ExitToken);

            TimeSpan phaseDuration = TimeSpan.FromSeconds(_volleyPhaseDuration);
            UniTask.Awaiter phaseEndAwaiter = UniTask.Delay(phaseDuration, cancellationToken: ExitToken).GetAwaiter();

            while (!phaseEndAwaiter.IsCompleted)
            {
                LaunchProjectiles().Forget();
                await UniTask.Delay(TimeSpan.FromSeconds(_volleyInterval), cancellationToken: ExitToken);
            }

            EnterAttackPhase().Forget();
        }

        private async UniTaskVoid LaunchProjectiles()
        {
            List<Vector3> projectilePositions = GetProjectilePositions();

            LayerMask playerMask = 1 << Player.PhysicsLayer;

            foreach (Vector3 position in projectilePositions)
            {
                Projectile projectile = Enemy.VfxSpawner.SpawnProjectile(position);
                ProjectileSettings settings = new(_projectileRadius, _projectileExplodeDelay, playerMask);

                projectile.ShootAsync(settings, () => Player.TakeDamage(Enemy.Damage)).Forget();
                AudioService.PlaySound(_shootSound);

                await UniTask.Delay(TimeSpan.FromSeconds(LaunchDelay), cancellationToken: ExitToken);
            }
        }

        private List<Vector3> GetProjectilePositions()
        {
            List<Vector3> positions = new List<Vector3>(_projectilesAmount);

            for (int i = 0; i < _projectilesAmount; i++)
            {
                Vector3 position = (UnityEngine.Random.insideUnitSphere * _volleyRange + Player.Position).WithZeroY();
                positions.Add(position);
            }

            return positions;
        }

        private bool CanAttackPlayer()
        {
            return IsPlayerInAttackRange() && IsPlayerInAttackZone();
        }

        private bool IsPlayerInAttackZone()
        {
            float rightAngle = Vector3.Angle(RightSideDirection, DirectionToPlayer);
            float leftAngle = Vector3.Angle(LeftSideDirection, DirectionToPlayer);

            return rightAngle <= HalfAttackAngle || leftAngle <= HalfAttackAngle;
        }

        private bool IsPlayerInAttackRange()
        {
            float distance = Vector3.Distance(Player.Position, Enemy.transform.position);

            return distance <= Config.AttackRange;
        }
    }
}