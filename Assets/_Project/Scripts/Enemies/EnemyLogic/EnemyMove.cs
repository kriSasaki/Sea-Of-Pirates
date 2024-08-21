using UnityEngine;

namespace Project.Enemies.EnemyLogic
{
    public class EnemyMove : MonoBehaviour
    {
        private const float MaxMagnitudeDelta = 0;
        
        [SerializeField] private float _speed = 3f;
        [SerializeField] private float _rotateSpeed = 1;
        
        private Vector3 _direction;
        
        public void Move(Vector3 target)
        {
            LookToTarget(target);
            transform.position = Vector3.MoveTowards(transform.position, target, _speed * Time.deltaTime);
        }
        
        private void LookToTarget(Vector3 target)
        {
            _direction = Vector3.RotateTowards(
                transform.forward,
                target - transform.position,
                _rotateSpeed,
                MaxMagnitudeDelta);
            transform.rotation = Quaternion.LookRotation(_direction);
        }
    }
}