using System;
using UnityEngine;

namespace Project.Enemies.EnemyLogic
{
    public class EnemyAttack : MonoBehaviour
    {
        [SerializeField] private Enemy _enemy;

        private float _baseAttackTime = 1;
        private float _attackTime => _baseAttackTime / _enemy.AttackSpeed;

        private Player _target;
        private float _lastAttackTime;

        public void SetTarget(Player target)
        {
            _target = target;
        }
        
        public void Attack()
        {
            if (_lastAttackTime <= 0)
            {
                _target.TakeDamage(_enemy.Damage);

                _lastAttackTime = _attackTime;
            }

            _lastAttackTime -= Time.deltaTime;
        }
    }
}