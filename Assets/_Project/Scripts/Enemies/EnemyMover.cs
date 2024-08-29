using Project.Configs.Enemies;
using UnityEngine;

namespace Project.Enemies
{
    public class EnemyMover
    {
        private readonly float _speed;
        private readonly Transform _transform;
        private readonly float _rotateSpeed = 50f;
        private readonly float _movingDotProduct = 0.1f;

        private Vector3 Position => _transform.position;

        public EnemyMover(EnemyConfig config, Transform transform)
        {
            _speed = config.Speed;
            _rotateSpeed = config.RotationSpeed;
            _transform = transform;
        }

        public void Move(Vector3 target)
        {
            LookToTarget(target);

            if (Vector3.Dot(_transform.forward, (target - Position).normalized) < _movingDotProduct)
                return;

            _transform.position = Vector3.MoveTowards(Position, target, _speed * Time.deltaTime);
        }

        private void LookToTarget(Vector3 target)
        {
            Vector3 direction = target - _transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, _rotateSpeed * Time.deltaTime);
        }
    }
}