using System;
using Project.Configs.Enemies;
using Project.Systems.Data;
using UnityEngine;

namespace Project.Interfaces.Enemies
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