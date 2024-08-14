using UnityEngine;
using DG.Tweening;

namespace Project.Enemies
{
    public class EnemyWandering : MonoBehaviour
    {
        [SerializeField] private float _movingDuration = 2f;
        [SerializeField] private float _lootAtDuration = 0.5f;

        private Vector3 _center;
        private float _movementRange;
        private Vector3 _newNextPosition;
        
        public void Initialize(Vector3 center , float movementRange)
        {
            _center = center;
            _movementRange = movementRange;
            StartMoving();
        }

        private void StartMoving()
        {
            _newNextPosition = Random.insideUnitSphere * _movementRange + _center;
            _newNextPosition = new Vector3(
                _newNextPosition.x,
                transform.position.y,
                _newNextPosition.z);
            transform.DOLookAt(_newNextPosition, _lootAtDuration);
            transform.DOMove(_newNextPosition, _movingDuration, false).OnComplete(StartMoving);
        }
    }
}