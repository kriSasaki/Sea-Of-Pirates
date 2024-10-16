using Scripts.Configs.Enemies;
using UnityEngine;

namespace Scripts.Enemies.Logic
{
    public class EnemyMover
    {
        private readonly Vector3 _rightAngle = new(0, -90, 0);
        private readonly Vector3 _leftAngle = new(0, 90, 0);

        private readonly float _speed;
        private readonly float _rotationSpeed;
        private readonly Transform _transform;
        private readonly float _moveAngleDot;

        public EnemyMover(EnemyConfig config, Transform transform)
        {
            _speed = config.Speed;
            _rotationSpeed = config.RotationSpeed;
            _moveAngleDot = config.MoveAngleDot;
            _transform = transform;
        }

        private Vector3 Position => _transform.position;

        public void Move(Vector3 target)
        {
            RotateTowards(target);

            if (Vector3.Dot(_transform.forward, (target - Position).normalized) <= _moveAngleDot)
                return;

            _transform.position = Vector3.MoveTowards(Position, target, _speed * Time.deltaTime);
        }

        public void RotateTowards(Vector3 target)
        {
            Vector3 direction = target - Position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            SetRotation(lookRotation);
        }

        public void RotateTowards(Vector3 target, BoardSide side)
        {
            Vector3 direction = (target - Position).normalized;

            Quaternion boardAngleOffset = side switch
            {
                BoardSide.Left => Quaternion.Euler(_leftAngle),
                BoardSide.Right => Quaternion.Euler(_rightAngle),
                _ => throw new System.NotImplementedException()
            };

            Quaternion lookRotation = Quaternion.LookRotation(direction) * boardAngleOffset;

            SetRotation(lookRotation);
        }

        private void SetRotation(Quaternion lookRotation)
        {
            float rotationDelta = _rotationSpeed * Time.deltaTime;

            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, rotationDelta);
        }
    }
}