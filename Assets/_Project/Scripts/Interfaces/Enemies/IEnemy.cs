using System;
using Scripts.Configs.Enemies;
using Scripts.Systems.Data;
using UnityEngine;

namespace Scripts.Interfaces.Enemies
{
    public interface IEnemy
    {
        event Action<IEnemy> Died;

        Vector3 Position { get; }
        bool IsAlive { get; }
        public GameResourceAmount Loot { get; }
        public EnemyConfig Config { get; }

        void TakeDamage(int damage);
    }
}