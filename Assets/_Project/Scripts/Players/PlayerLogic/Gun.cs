using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Project.Enemies;
using Project.Interfaces.Audio;
using Zenject;

namespace Project.Players.PlayerLogic
{
    public class Gun : MonoBehaviour
    {
        [SerializeField] private GameObject _particleObject;
        [SerializeField] private float _attackCoolDown = 1f;
        [SerializeField] private float _minimalAudioPitch;
        [SerializeField] private float _maximalAudioPitch;
        [SerializeField] private float _effectTime;
        [SerializeField] private PlayerAttack _playerAttack;
        [SerializeField] private AudioClip _audioClip;

        private IAudioService _audioService;
        private float _attackRange;
        private float _damage;
        private List<Enemy> _enemiesInRange = new List<Enemy>();
        private Coroutine _attackCoroutine;

        [Inject]
        public void Construct(IAudioService audioService)
        {        
            _audioService = audioService;
        }

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
                if (_attackCoroutine == null)
                {
                    _attackCoroutine = StartCoroutine(StartAttack());
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Enemy>(out Enemy enemy))
            {
                _enemiesInRange.Remove(enemy);
                if (_enemiesInRange.Count == 0 && _attackCoroutine != null)
                {
                    StopCoroutine(_attackCoroutine);
                    _attackCoroutine = null;
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
                    _audioService.PlaySound(_audioClip);
                    _particleObject.SetActive(true);
                    Invoke(nameof(HideFlash), _effectTime);
                    nearestEnemy.TakeDamage((int)_damage);
                }
            }
        }

        private IEnumerator StartAttack()
        {
            while (_enemiesInRange.Count > 0)
            {
                Attack();
                yield return new WaitForSeconds(_attackCoolDown);
            }
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
