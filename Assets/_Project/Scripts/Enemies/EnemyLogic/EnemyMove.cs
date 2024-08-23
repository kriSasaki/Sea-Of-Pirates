using Project.Enemies.EnemyLogic.StateMachine;
using UnityEngine;

namespace Project.Enemies.EnemyLogic
{
    public class EnemyMove : State
    {
        private readonly float _speed;
        private readonly float _rotateSpeed;
        private readonly float _maxMagnitudeDelta;

        protected Transform Transform;

        private Vector3 _direction;

        public EnemyMove(StateMachine.StateMachine stateMachine, Transform transform, float speed, float rotateSpeed,
            float maxMagnitudeDelta) : base(stateMachine)
        {
            Transform = transform;
            _speed = speed;
            _rotateSpeed = rotateSpeed;
            _maxMagnitudeDelta = maxMagnitudeDelta;
        }

        public void Move(Vector3 target)
        {
            LookToTarget(target);
            Transform.position = Vector3.MoveTowards(Transform.position, target, _speed * Time.deltaTime);
        }

        private void LookToTarget(Vector3 target)
        {
            _direction = Vector3.RotateTowards(
                Transform.forward,
                target - Transform.position,
                _rotateSpeed,
                _maxMagnitudeDelta);
            Transform.rotation = Quaternion.LookRotation(_direction);
        }
    }
}