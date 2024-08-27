using Project.Enemies.EnemyLogic.StateMachine;
using Project.Players.Logic;
using UnityEngine;

namespace Project.Enemies.EnemyLogic
{
    public class EnemyAttack : State
    {
        private readonly Transform _transform;
        private readonly float _rangeToAttack;
        private readonly int _damage;
        private float _attackInterval;

        private float _lastAttackTime;
        private float _distanceFromTarget;

        public EnemyAttack(StateMachine.StateMachine stateMachine, Transform transform, Player target,
            float rangeToAttack, int damage, float attackInterval) : base(stateMachine)
        {
            _transform = transform;
            _target = target;
            _rangeToAttack = rangeToAttack;
            _damage = damage;
            _attackInterval = attackInterval;
        }

        public override void Update()
        {
            _distanceFromTarget = (_transform.position - _target.transform.position).magnitude;

            if (_distanceFromTarget > _rangeToAttack)
            {
                _stateMachine.SetState<EnemyChase>();
            }

            Attack();
        }

        public void Attack()
        {
            if (_lastAttackTime <= 0)
            {
                _target.TakeDamage(_damage);

                _lastAttackTime = _attackInterval;
            }

            _lastAttackTime -= Time.deltaTime;
        }
    }
}

