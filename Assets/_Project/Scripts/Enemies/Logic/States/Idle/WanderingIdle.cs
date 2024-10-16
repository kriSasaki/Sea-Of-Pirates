using Scripts.Utils.Extensions;
using UnityEngine;

namespace Scripts.Enemies.Logic.States.Idle
{
    [CreateAssetMenu(fileName = "WanderingIdle", menuName = "Configs/Enemies/States/WanderingIdle")]

    public class WanderingIdle : IdleState
    {
        private const float DistanceTrashhold = 0.001f;

        [SerializeField, Min(1f)] private float _movementRange = 15f;
        [SerializeField] private LayerMask _obstacleMask;

        private Vector3 _targetPosition;

        private Vector3 SpawnPosition => Enemy.SpawnPosition;
        private Vector3 Position => Enemy.Position;

        public override void Enter()
        {
            base.Enter();

            _targetPosition = SpawnPosition;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            if (Vector3.SqrMagnitude(_targetPosition - Position) < DistanceTrashhold)
                SetTargetPosition();

            Enemy.Mover.Move(_targetPosition);
        }

        private void SetTargetPosition()
        {
            Vector3 newPosition = GetRandomPosition();

            while (IsValidPosition(newPosition) == false)
            {
                newPosition = GetRandomPosition();
            }

            _targetPosition = newPosition;
        }

        private bool IsValidPosition(Vector3 position)
        {
            Bounds shipBounds = Enemy.ShipCollider.bounds;
            Vector3 direction = position - Enemy.Position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            float distance = direction.magnitude;

            bool hasObstacles = Physics.BoxCast(
                shipBounds.center,
                shipBounds.extents,
                direction,
                lookRotation,
                distance,
                _obstacleMask);

            return hasObstacles == false;
        }

        private Vector3 GetRandomPosition()
        {
            return (Random.insideUnitSphere * _movementRange + SpawnPosition).WithZeroY();
        }
    }
}