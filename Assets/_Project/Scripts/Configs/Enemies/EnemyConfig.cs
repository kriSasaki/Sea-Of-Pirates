using Project.Systems.Data;
using NaughtyAttributes;
using UnityEngine;
using Project.Enemies.View;

namespace Project.Configs.Enemies
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Configs/Enemies/Enemy")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField, Min(1)] private int _maxHealth;
        [SerializeField] private bool _canDealDamage = false;
        [SerializeField,ShowIf(nameof(_canDealDamage)) , Min(0)] private int _damage = 0;
        [SerializeField, ShowIf(nameof(_canDealDamage)), Min(0.1f)] private float _attackCooldown;
        [HorizontalLine(2f, EColor.Blue)]
        [SerializeField, Min(0.1f)] private float _speed;
        [SerializeField, Range(30f, 100f)] private float _rotationSpeed;
        [SerializeField, Range(0.1f, 0.8f)] private float _moveAngleDot = 0.7f;
        [HorizontalLine(2f, EColor.Blue)]
        [SerializeField, ShowIf(nameof(_canDealDamage))] private float _attackRange;
        [SerializeField] private float _detectRange;
        [HorizontalLine(2f, EColor.Blue)]
        [SerializeField] private EnemyView _enemyView;
        [SerializeField] private bool _isSolidForPlayer = false;
        [SerializeField, Expandable] private EnemyBehaviorConfig _behaviourConfig;
        [SerializeField] private GameResourceAmount _loot;

        public int MaxHealth => _maxHealth;
        public float Speed => _speed;
        public float RotationSpeed => _rotationSpeed;
        public float MoveAngleDot => _moveAngleDot;
        public int Damage => _damage;
        public float AttackCooldown => _attackCooldown;
        public float AttackRange => _attackRange;
        public float DetectRange => _detectRange;
        public bool CanDealDamage => _canDealDamage;
        public EnemyView View => _enemyView;
        public bool IsSolidForPlayer => _isSolidForPlayer;
        public EnemyBehaviorConfig BehaviorConfig => _behaviourConfig;
        public GameResourceAmount Loot => _loot;
    }
}
