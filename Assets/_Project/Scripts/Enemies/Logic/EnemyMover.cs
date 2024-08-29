using Project.Configs.Enemies;
using UnityEngine;

namespace Project.Enemies.Logic
{
    public class EnemyMover
    {
        private readonly float _speed;
        private readonly float _rotationSpeed;
        private readonly Transform _transform;
        private readonly float _moveAngleDot;

        private Vector3 Position => _transform.position;

        public EnemyMover(EnemyConfig config, Transform transform)
        {
            _speed = config.Speed;
            _rotationSpeed = config.RotationSpeed;
            _moveAngleDot = config.MoveAngleDot;
            _transform = transform;
        }

        public void Move(Vector3 target)
        {
            RotateTowards(target);

            if (Vector3.Dot(_transform.forward, (target - Position).normalized) <= _moveAngleDot)
                return;

            _transform.position = Vector3.MoveTowards(Position, target, _speed * Time.deltaTime);
        }

        private void RotateTowards(Vector3 target)
        {
            Vector3 direction = target - Position;
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            _transform.rotation = Quaternion.RotateTowards(_transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);
        }
    }
}