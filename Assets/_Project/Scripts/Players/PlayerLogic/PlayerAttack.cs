using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Project.Enemies;
using Project.Interfaces.Audio;
using Project.Interfaces.Stats;
using UnityEngine;
using Zenject;

namespace Project.Players.PlayerLogic
{
    [RequireComponent(typeof(SphereCollider))]
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private PlayerAttackView _attackView;
        [SerializeField] private float _attackAngle = 100;
        [SerializeField] private float _attackCooldown = 2f;
        [SerializeField] private AudioClip _shootSound;

        private readonly List<Enemy> _detectedEnemies = new();

        private SphereCollider _detectZone;
        private IPlayerStats _playerStats;
        private IAudioService _audioService;
        private Coroutine _battleCoroutine;
        private WaitUntil _hasTargetAwaiter;

        public int Damage => _playerStats.Damage;
        public int AttackRange => _playerStats.AttackRange;
        public int CannonsAmount => _playerStats.CannonsAmount;

        private bool HasDetectedEnemies => _detectedEnemies.Count > 0;
        private bool IsInBattle => _battleCoroutine != null;
        private float HalfAttackAnlge => _attackAngle / 2f;

        private void Start()
        {
            SetAttackZone();
        }

        private void OnDestroy()
        {
            _playerStats.StatsUpdated -= OnStatsUpdated;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Enemy>(out Enemy enemy))
            {
                _detectedEnemies.Add(enemy);

                if (IsInBattle == false)
                    EnterBattle();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Enemy>(out Enemy enemy))
            {
                _detectedEnemies.Remove(enemy);

                if (HasDetectedEnemies == false)
                    ExitBattle();
            }
        }

        [Inject]
        private void Construct(
            IPlayerStats playerStats,
            IAudioService audioService)
        {
            _detectZone = GetComponent<SphereCollider>();
            _playerStats = playerStats;
            _audioService = audioService;
            _hasTargetAwaiter = new WaitUntil(HasTargetEnemines);

            _playerStats.StatsUpdated += OnStatsUpdated;
        }

        private void SetAttackZone()
        {
            _detectZone.radius = AttackRange;
            _attackView.SetRange(AttackRange);
            _attackView.SetAngle(_attackAngle);
        }

        private void EnterBattle()
        {
            _battleCoroutine = StartCoroutine(Battle());
            _attackView.Show();
        }

        private void ExitBattle()
        {
            ClearBattleCoroutine();
            _attackView.Hide();
        }

        private void ClearBattleCoroutine()
        {
            if (_battleCoroutine != null)
                StopCoroutine(_battleCoroutine);

            _battleCoroutine = null;
        }

        private IEnumerator Battle()
        {
            while (HasDetectedEnemies)
            {
                yield return _attackView.Reloading(_attackCooldown);

                CheckEnemies();

                yield return _hasTargetAwaiter;

                foreach (Enemy enemy in GetTargetEnemies())
                {
                    _attackView.Shoot(enemy.transform.position);
                    enemy.TakeDamage(Damage);
                }

                _audioService.PlaySound(_shootSound);

                yield return null;

                CheckEnemies();
            }

            ExitBattle();
        }

        private bool HasTargetEnemines()
        {
            return _detectedEnemies.Any(e => CanAttackEnemy(e));
        }

        private IEnumerable<Enemy> GetTargetEnemies()
        {
            return _detectedEnemies
                .Where(e => CanAttackEnemy(e))
                .OrderBy(e => Vector3.SqrMagnitude(e.transform.position - transform.position))
                .Take(Mathf.Min(_detectedEnemies.Count(), CannonsAmount));
        }

        private bool CanAttackEnemy(Enemy enemy)
        {
            Vector3 direction = enemy.transform.position - transform.position;
            float rightAngle = Vector3.Angle(transform.right, direction);
            float leftAngle = Vector3.Angle(-transform.right, direction);

            return rightAngle <= HalfAttackAnlge || leftAngle <= HalfAttackAnlge;
        }

        private void CheckEnemies()
        {
            for (int i = _detectedEnemies.Count - 1; i >= 0; i--)
            {
                Enemy enemy = _detectedEnemies[i];

                if (enemy == null || enemy.gameObject.activeInHierarchy == false)
                    _detectedEnemies.Remove(enemy);
            }
        }

        private void OnStatsUpdated()
        {
            SetAttackZone();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            DrawAttackZones(_attackAngle, 25f);
        }

        private void DrawAttackZones(float angle, float range)
        {
            DrawCone(angle, transform.right, range);
            DrawCone(angle, -transform.right, range);
        }

        private void DrawCone(float angle, Vector3 direction, float range)
        {
            Quaternion rotation = Quaternion.AngleAxis(angle / 2f, Vector3.up);
            Gizmos.DrawRay(transform.position, rotation * direction * range);

            rotation = Quaternion.AngleAxis(-angle / 2f, Vector3.up);
            Gizmos.DrawRay(transform.position, rotation * direction * range);
        }
    }
}