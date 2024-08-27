using Project.Players.Logic;
using UnityEngine;

namespace Project.Enemies.EnemyLogic.StateMachine
{
    public class EnemyStateMachine : MonoBehaviour
    {
        private const float MaxMagnitudeDelta = 0;

        [SerializeField] private Enemy _enemy;
        [SerializeField] private float _speed = 3f;
        [SerializeField] private float _rotateSpeed = 1;
        [SerializeField] private float _maxDistanceFromSpawn = 30;
        [SerializeField] private float _rangeToAttack = 5;

        private StateMachine _stateMachine;
        private Player _target;

        private void Start()
        {
            _stateMachine = new StateMachine();

            _stateMachine.AddState(new EnemyWaiting(_stateMachine, transform.position, transform, _speed,
                _rotateSpeed, MaxMagnitudeDelta));

            _stateMachine.SetState<EnemyWaiting>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                if (_target == null)
                {
                    _target = player;
                    _stateMachine.AddState(new EnemyChase(_stateMachine, transform, _target,
                        transform.position, _maxDistanceFromSpawn, _speed, _rotateSpeed,
                        MaxMagnitudeDelta, _rangeToAttack));
                    _stateMachine.AddState(new EnemyAttack(_stateMachine, transform, _target, _rangeToAttack,
                        _enemy.Damage,
                        _enemy.AttackInterval));
                }

                _stateMachine.SetState<EnemyChase>();
            }
        }

        private void Update()
        {
            _stateMachine.Update();
        }
    }
}