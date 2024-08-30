using Project.Players.Logic;
using Unity.Profiling;
using UnityEngine;

namespace Project.Enemies.Logic.States.Battle
{
    [CreateAssetMenu(fileName = "SimpleAttack", menuName = "Configs/Enemies/States/SimpleAttack")]
    public class AttackBattle : BattleState
    {
        [SerializeField] private float _attackAngle = 60;

        private float _timeToAttack;

        private float HalfAttackAngle => _attackAngle / 2f;
        private Vector3 DirectionToPlayer => Player.Position - Enemy.transform.position;
        private Vector3 RightSideDirection => Enemy.transform.right;
        private Vector3 LeftSideDirection => -Enemy.transform.right;

        public override void Enter()
        {
            base.Enter();
            ResetCooldown();
        }

        public override void Update()
        {
            base.Update();

            Enemy.Mover.RotateTowards(Player.Position, GetClosestSide());

            _timeToAttack -= Time.deltaTime;

            if (CanAttackPlayer())
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            Enemy.DealDamageAsync(Player).Forget();
            ResetCooldown();
        }

        private void ResetCooldown()
        {
            _timeToAttack = Config.AttackCooldown;
        }

        private BoardSide GetClosestSide()
        {
            float rightDot = Vector3.Dot(RightSideDirection, DirectionToPlayer);
            float leftDot = Vector3.Dot(LeftSideDirection, DirectionToPlayer);

            return rightDot >= leftDot ? BoardSide.Right : BoardSide.Left;
        }

        private bool CanAttackPlayer()
        {
            if (Player.IsAlive == false)
                return false;

            return IsCannonsLoaded() && IsPlayerInAttackRange() && IsPlayerInAttackZone();
        }

        private bool IsPlayerInAttackRange()
        {
            float distance = Vector3.Distance(Player.Position, Enemy.transform.position);

            return distance <= Config.AttackRange;
        }

        private bool IsCannonsLoaded()
        {
            return _timeToAttack <= 0;
        }

        private bool IsPlayerInAttackZone()
        {
            float rightAngle = Vector3.Angle(RightSideDirection, DirectionToPlayer);
            float leftAngle = Vector3.Angle(LeftSideDirection, DirectionToPlayer);

            return rightAngle <= HalfAttackAngle || leftAngle <= HalfAttackAngle;
        }
    }
}