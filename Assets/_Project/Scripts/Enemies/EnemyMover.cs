using Project.Configs.Enemies;
using UnityEngine;

namespace Project.Enemies
{
    public class EnemyMover
    {
        private readonly float _speed;
        private readonly Transform _transform;
        private readonly float _rotateSpeed = 0.01f;
        private readonly float _movingDotProduct = 0.1f;

        private Vector3 Forward => _transform.forward;
        private Vector3 Position => _transform.position;

        public EnemyMover(EnemyConfig config, Transform transform)
        {
            _speed = config.Speed;
            _transform = transform;
        }

        public void Move(Vector3 target)
        {
            LookToTarget(target);
            if (Vector3.Dot(Forward, target - Position) > _movingDotProduct)
                _transform.position = Vector3.MoveTowards(Position, target, _speed * Time.deltaTime);
        }

        private void LookToTarget(Vector3 target)
        {
            Vector3 direction = target - Position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            //_transform.rotation = Quaternion.RotateTowards(_transform.rotation, rotation, _rotateSpeed * Time.deltaTime);
            ////_transform.rotation = Quaternion.Lerp(_transform.rotation, rotation, _) ;
            //_transform.forward =  Vector3.Slerp(Forward, direction, _rotateSpeed * Time.deltaTime);
            Vector3 lookDirection = Vector3.RotateTowards(Forward, direction, _rotateSpeed, 1f);
            _transform.rotation = Quaternion.LookRotation(lookDirection);
            //_transform.rotation = Quaternion.RotateTowards(_transform.rotation, direction);
        }
    }
}