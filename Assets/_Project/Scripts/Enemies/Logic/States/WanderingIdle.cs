using Project.Utils.Extensions;
using UnityEngine;

namespace Project.Enemies.Logic.States
{
    [CreateAssetMenu(fileName = "WanderingIdle", menuName = "Configs/Enemies/States/WanderingIdle")]

    public class WanderingIdle : IdleState
    {
        [SerializeField,Min(1f)] private float _movementRange = 15f;
        [SerializeField] private LayerMask _obstacleMask;

        private Vector3 _targetPosition;

        private Vector3 SpawnPosition => Enemy.SpawnPosition;
        private Vector3 Position => Enemy.Position;
        private Bounds ShipBounds => Config.ShipView.ShipBounds;

        public override void Enter()
        {
            base.Enter();
            SetTargetPosition();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void Update()
        {
            base.Update();

            Enemy.Mover.Move(_targetPosition);

            if (Position == _targetPosition)
                SetTargetPosition();
        }

        private void SetTargetPosition()
        {
            Vector3 newPosition = GetRandomPosition();

            while (Physics.CheckBox(newPosition, ShipBounds.extents, Quaternion.identity, _obstacleMask))
            {
                newPosition = GetRandomPosition();
            }

            _targetPosition = newPosition;
        }

        private Vector3 GetRandomPosition()
        {
            return (Random.insideUnitSphere * _movementRange + SpawnPosition).WithY(Position.y);
        }
    }
}