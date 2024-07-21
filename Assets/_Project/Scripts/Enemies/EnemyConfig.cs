using Project.Configs.GameResources;
using UnityEngine;

namespace Project.Enemies
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private int _health;
        [SerializeField] private int _damage;
        [SerializeField] private GameResource _gameResource;
        [SerializeField] private int _resourceAmount;

        public int Health => _health;
        public int Damage => _damage;
        public GameResource GameResource => _gameResource;
        public int ResourceAmount => _resourceAmount;
    }
}