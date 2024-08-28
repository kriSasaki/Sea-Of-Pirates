using System;
using Cysharp.Threading.Tasks;
using Project.Configs.Enemies;
using Project.Enemies.View;
using Project.Interfaces.Enemies;
using Project.Players.Logic;
using Project.Spawner;
using Project.Systems.Data;
using UnityEngine;

namespace Project.Enemies
{
    public class Enemy : MonoBehaviour, IPoolableEnemy
    {
        private const int MinimumHealth = 0;

        [SerializeField] BoxCollider _boxCollider;
        [SerializeField] private EnemyView _enemyView;
        [SerializeField] private PlayerDetector _playerDetector;

        private EnemyConfig _config;
        private VfxSpawner _vfxSpawner;
        private int _currentHealth;
        private EnemyMover _mover;
        private EnemyStateMachine _stateMachine;

        public event Action<IEnemy> Died;
        public event Action PlayerDetected;
        public event Action PlayerLost;
        public event Action Respawned;


        public int Damage => _config.Damage;
        public float AttackInterval => _config.AttackInterval;
        public bool IsAlive => _currentHealth > MinimumHealth;

        public EnemyMover Mover => _mover;
        public Vector3 Position => transform.position;
        public GameResourceAmount Loot => _config.Loot;
        public EnemyConfig Config => _config;
        public Vector3 SpawnPosition { get; private set; }

        private void Update()
        {
            _stateMachine.Update();
        }

        private void OnDestroy()
        {
            _playerDetector.PlayerDetected -= OnPlayerDetected;
            _playerDetector.PlayerLost -= OnPlayerLost;
        }

        public void Initialize(
            EnemyConfig config, 
            VfxSpawner vfxSpawner,
            EnemyStateMachine stateMachine)
        {
            _config = config;
            _vfxSpawner = vfxSpawner;
            _currentHealth = config.MaxHealth;
            _stateMachine = stateMachine;
            _mover = new EnemyMover(_config, transform);

            SpawnPosition = transform.position;
            name = config.name;

            config.ShipView.SetShipColliderSize(_boxCollider);
            _enemyView.Initialize(_config.ShipView);
            _playerDetector.Initialize(_config.DetectRange);
            _stateMachine.Initialize(this);

            _playerDetector.PlayerDetected += OnPlayerDetected;
            _playerDetector.PlayerLost += OnPlayerLost;
        }




        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _vfxSpawner.SpawnExplosion(transform.position);

            if (!IsAlive)
            {
                Die();
                return;
            }

        }

        public void Respawn(Vector3 atPosition)
        {
            gameObject.SetActive(true);
            transform.position = atPosition;
            SpawnPosition = transform.position;
            _currentHealth = _config.MaxHealth;
            _enemyView.Ressurect();
            Respawned?.Invoke();
        }

        private void Die()
        {
            Died?.Invoke(this);
        }

        private void Deactivate()
        {
            gameObject.SetActive(false);
        }

        public async UniTask SinkAsync()
        {
            await _enemyView.DieAsync();
            Deactivate();
        }

        private void OnPlayerLost()
            => PlayerLost?.Invoke();

        private void OnPlayerDetected()
            => PlayerDetected?.Invoke();
    }
}