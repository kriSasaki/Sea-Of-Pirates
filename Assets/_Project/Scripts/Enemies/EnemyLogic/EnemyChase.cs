using Project.Players.Logic;
using UnityEngine;

namespace Project.Enemies.EnemyLogic
{
    public class EnemyChase : EnemyMove
    {
        private readonly Vector3 _spawnPosition;
        private readonly float _maxDistanceFromSpawn;
        private readonly float _rangeToAttack;

        private float _distanceFromSpawn;
        private float _distanceFromTarget;

        public EnemyChase(StateMachine.StateMachine stateMachine, Transform transform, Player target,
            Vector3 spawnPosition, float maxDistanceFromSpawn, float speed, float rotateSpeed,
            float maxMagnitudeDelta, float rangeToAttack) : base(stateMachine, transform, speed, rotateSpeed,
            maxMagnitudeDelta)
        {
            Transform = transform;
            _target = target;
            _spawnPosition = spawnPosition;
            _maxDistanceFromSpawn = maxDistanceFromSpawn;
            _rangeToAttack = rangeToAttack;
        }

        public override void Update()
        {
            _distanceFromTarget = (Transform.position - _target.transform.position).magnitude;
            _distanceFromSpawn = (_spawnPosition - Transform.position).magnitude;

            if (_distanceFromSpawn <= _maxDistanceFromSpawn)
            {
                if (_distanceFromTarget <= _rangeToAttack)
                {
                    _stateMachine.SetState<EnemyAttack>();
                }
                else
                {
                    Move(_target.transform.position);
                }
            }
            else
            {
                _stateMachine.SetState<EnemyWaiting>();
            }
        }
    }
}