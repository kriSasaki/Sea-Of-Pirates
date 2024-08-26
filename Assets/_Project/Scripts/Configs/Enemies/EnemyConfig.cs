using Project.Systems.Data;
using UnityEngine;

namespace Project.Configs.Enemies
{
    [CreateAssetMenu(fileName = "Enemy", menuName = "Scriptable Objects/Enemy")]
    public class EnemyConfig : ScriptableObject
    {
        [field:SerializeField] public int MaxHealth { get;private set; }
        [field:SerializeField] public int Damage { get;private set; }
        [field:SerializeField] public float AttackInterval { get;private set; }
        [field:SerializeField] public float Speed { get;private set; }

        [field: SerializeField] public GameResourceAmount Loot { get; private set; }


    }
}