using System;
using Project.Enemies;

namespace Project.Interfaces.Enemies
{
    public interface IEnemyDeathNotifier
    {
        event Action<EnemyConfig> EnemyDied;
    }
}