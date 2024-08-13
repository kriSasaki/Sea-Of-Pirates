using UnityEngine;
using System.Collections.Generic;
using Project.Enemies;
using System.Collections;

namespace Project.Players.PlayerLogic
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private GameObject _particleObject;
        [SerializeField] private float _àttackCoolDown = 1f;
        [SerializeField] private AudioSource _soundOfGunshot;
        [SerializeField] private float _minimalAudioPitch;
        [SerializeField] private float _maximalAudioPitch;
        [SerializeField] private float _effectTime;
        [SerializeField] private PlayerAttack _playerAttack;
        private float _attackRange;
        private float _damage;

        private List<Enemy> _enemiesInRange = new List<Enemy>();
        private bool _canAttack = false;
        private bool _isAttacking;

        private void Start()
        {
            _attackRange = _playerAttack.AttackRange;
            _damage = _playerAttack.Damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Enemy>(out Enemy enemy))
            {
                _enemiesInRange.Add(enemy);
                _canAttack = true;
                StartCoroutine(StartAttack());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Enemy>(out Enemy enemy))
            {
                _enemiesInRange.Remove(enemy);
                if (_enemiesInRange.Count == 0)
                {
                    _canAttack = false;
                }
            }
        }

        public void Attack()
        {
            Enemy nearestEnemy = FindClosestEnemy();
            if (nearestEnemy != null)
            {
                float distance = Vector3.Distance(transform.position, nearestEnemy.transform.position);
                if (distance <= _attackRange)
                {
                    _soundOfGunshot.pitch = Random.Range(_minimalAudioPitch, _maximalAudioPitch);
                    _particleObject.SetActive(true);
                    Invoke(nameof(HideFlash), _effectTime);

                    nearestEnemy.TakeDamage((int)_damage);
                }
            }
        }

        private IEnumerator StartAttack()
        {
            _isAttacking = true;
            while (_isAttacking)
            {
                if (CanAttack())
                {
                    Attack();
                    yield return new WaitForSeconds(_àttackCoolDown);
                }
            }
        }

        private bool CanAttack()
        {
            return !_isAttacking && CoolDownIsUp();
        }

        private bool CoolDownIsUp()
        {
            return _àttackCoolDown <= 0;
        }

        private Enemy FindClosestEnemy()
        {
            float minDistance = Mathf.Infinity;
            Enemy closestEnemy = null;

            foreach (Enemy enemy in _enemiesInRange)
            {
                if (enemy != null)
                {
                    float distance = Vector3.Distance(transform.position, enemy.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        closestEnemy = enemy;
                    }
                }
            }

            return closestEnemy;
        }

        private void HideFlash()
        {
            _particleObject.SetActive(false);
        }
    }
}