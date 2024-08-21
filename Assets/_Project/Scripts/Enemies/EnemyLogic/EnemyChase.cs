using UnityEngine;

namespace Project.Enemies.EnemyLogic
{
    public class EnemyChase : MonoBehaviour
    {
        [SerializeField] private EnemyMove _enemyMove;
        [SerializeField] private EnemyWaiting _enemyWaiting;
        [SerializeField] private EnemyAttack _enemyAttack;
        [SerializeField] private float _maxDistanceFromSpawn = 30;
        [SerializeField] private float _rangeToAttack = 5;
        [SerializeField] private float _speed = 2f;

        private Player _target;
        private Vector3 _spawnPosition;
        private float _distanceFromSpawn;
        private float _distanceFromTarget;

        private void Awake()
        {
            _spawnPosition = transform.position;
            enabled = false;
        }

        private void Update()
        {
            _distanceFromTarget = (transform.position - _target.transform.position).magnitude;
            _distanceFromSpawn = (_spawnPosition - transform.position).magnitude;

            if (_distanceFromSpawn <= _maxDistanceFromSpawn)
            {
                if (_distanceFromTarget <= _rangeToAttack)
                {
                    _enemyAttack.Attack();
                }
                else
                {
                    _enemyMove.Move(_target.transform.position);
                }
            }
            else
            {
                enabled = false;
                _enemyWaiting.StartWaiting();
            }
        }

        public void StartChase(Player target)
        {
            _target = target;
            enabled = true;
        }
    }
}