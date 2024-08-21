using UnityEngine;
using DG.Tweening;
using Random = UnityEngine.Random;

namespace Project.Enemies.EnemyLogic
{
    public class EnemyWandering : MonoBehaviour
    {
        [SerializeField] private float _movingDuration = 2f;
        [SerializeField] private float _lookAtDuration = 0.5f;
        [SerializeField] private float _movementRange = 5f;
        
        private Vector3 _newNextPosition;
        private Vector3 _startPosition;

        private void Awake()
        {
            _startPosition = transform.position;
            StartMoving();
        }

        private void StartMoving()
        {
            _newNextPosition = Random.insideUnitSphere * _movementRange + _startPosition;
            _newNextPosition = new Vector3(
                _newNextPosition.x,
                transform.position.y,
                _newNextPosition.z);
            transform.DOLookAt(_newNextPosition, _lookAtDuration);
            transform.DOMove(_newNextPosition, _movingDuration, false).OnComplete(StartMoving);
        }
    }
}