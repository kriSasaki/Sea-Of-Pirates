using System;
using Cysharp.Threading.Tasks;
using Project.Configs.Enemies;
using Project.Enemies.View;
using Project.Interfaces.Enemies;
using Project.Players.Logic;
using Project.Spawner;
using Project.Systems.Data;
using UnityEngine;

namespace Project.Enemies.Logic
{
    [RequireComponent(typeof(EnemyStateMachine), typeof(BoxCollider))]
    public class Enemy : MonoBehaviour, IPoolableEnemy
    {
        private const int MinimumHealth = 0;

        [SerializeField] private EnemyView _view;
        [SerializeField] private PlayerDetector _playerDetector;

        private BoxCollider _shipCollider;
        private EnemyConfig _config;
        private VfxSpawner _vfxSpawner;
        private EnemyMover _mover;
        private EnemyStateMachine _stateMachine;
        private int _currentHealth;

        public event Action<IEnemy> Died;
        public event Action Respawned;
        public event Action Damaged;

        public bool IsAlive => _currentHealth > MinimumHealth;
        public int Damage => _config.Damage;
        public float AttackInterval => _config.AttackCooldown;

        public EnemyConfig Config => _config;
        public EnemyMover Mover => _mover;
        public Collider ShipCollider => _shipCollider;
        public PlayerDetector Detector => _playerDetector;
        public Vector3 Position => transform.position;
        public GameResourceAmount Loot => _config.Loot;
        public Vector3 SpawnPosition { get; private set; }

        public void Initialize(
            EnemyConfig config,
            VfxSpawner vfxSpawner,
            Player player)
        {
            _shipCollider = GetComponent<BoxCollider>();
            _config = config;
            _vfxSpawner = vfxSpawner;
            _currentHealth = config.MaxHealth;
            _stateMachine = GetComponent<EnemyStateMachine>();
            _mover = new EnemyMover(_config, transform);

            config.ShipView.SetShipColliderSize(_shipCollider);
            SetSpawnPosition();

            _view.Initialize(_config.ShipView, _vfxSpawner);
            _playerDetector.Initialize(_config.DetectRange);
            _stateMachine.Initialize(player);
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _view.TakeDamage();

            Damaged?.Invoke();

            if (!IsAlive)
                Die();
        }

        public void HitPlayer (Player player)
        {
            _view.Shoot(player.transform.position);
            player.TakeDamage(Damage);
        }

        public void Respawn(Vector3 atPosition)
        {
            gameObject.SetActive(true);
            transform.position = atPosition;
            SetSpawnPosition();
            RestoreHealth();
            _view.Ressurect();
            Respawned?.Invoke();
        }

        private void RestoreHealth()
        {
            _currentHealth = _config.MaxHealth;
        }

        private void SetSpawnPosition()
        {
            SpawnPosition = transform.position;
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
            await _view.DieAsync();
            Deactivate();
        }
    }
}