using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Project.Enemies;
using Project.Players.Inputs;
using Zenject;
using Project.Interfaces.Stats;
using System;
using System.Linq;
using DTT.AreaOfEffectRegions;

namespace Project.Players.PlayerLogic
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private SphereCollider _attackZone;
        [SerializeField] private CircleRegion _attackView;
        [SerializeField] private float _attackCooldown = 2f;

        private IPlayerStats _playerStats;


        private List<Enemy> _enemiesInZone = new();
        private Coroutine _battleCoroutine;
        private WaitForSeconds _attackDelay;

        public int Damage => _playerStats.Damage;
        public int AttackRange => _playerStats.AttackRange;
        public int CannonsAmount => _playerStats.CannonsAmount;

        private bool IsEnemiesInZone => _enemiesInZone.Count > 0;
        private bool IsInBattle => _battleCoroutine != null;

        private void Start()
        {
            SetAttackZone();
            ExitBattle();
        }

        private void OnDestroy()
        {
            _playerStats.StatsUpdated -= OnStatsUpdated;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Enemy>(out Enemy enemy))
            {
                _enemiesInZone.Add(enemy);

                if (IsInBattle == false)
                    EnterBattle();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Enemy>(out Enemy enemy))
            {
                _enemiesInZone.Remove(enemy);

                if (IsEnemiesInZone == false)
                    ExitBattle();
            }
        }

        [Inject]
        private void Construct(IPlayerStats playerStats)
        {
            _playerStats = playerStats;
            _playerStats.StatsUpdated += OnStatsUpdated;

            _attackDelay = new WaitForSeconds(_attackCooldown);
        }

        private void SetAttackZone()
        {
            _attackZone.radius = AttackRange;
            _attackView.Radius = AttackRange;
        }

        private void EnterBattle()
        {
            _battleCoroutine = StartCoroutine(Battle());
            _attackView.gameObject.SetActive(true);
        }

        private void ExitBattle()
        {
            ClearBattleCoroutine();
            _attackView.gameObject.SetActive(false);
        }

        private IEnumerator Battle()
        {
            while (IsEnemiesInZone)
            {
                yield return _attackDelay;
                
                foreach (Enemy enemy in GetTargetEnemies())
                {
                    enemy.TakeDamage(Damage);
                }

                yield return null;
                CheckEnemies();
            }

            ExitBattle();
        }

        private IEnumerable<Enemy> GetTargetEnemies()
        {
            return _enemiesInZone
                .OrderBy(e => Vector3.SqrMagnitude(e.transform.position - transform.position))
                .Take(Mathf.Min(_enemiesInZone.Count, CannonsAmount));
        }

        private void CheckEnemies()
        {
            for (int i = _enemiesInZone.Count - 1; i >= 0; i--)
            {
                if (_enemiesInZone[i] == null)
                    _enemiesInZone.RemoveAt(i);
            }
        }

        private void ClearBattleCoroutine()
        {
            if (_battleCoroutine != null)
                StopCoroutine(_battleCoroutine);

            _battleCoroutine = null;
        }

        private void OnStatsUpdated()
        {
            SetAttackZone();
        }
    }
}