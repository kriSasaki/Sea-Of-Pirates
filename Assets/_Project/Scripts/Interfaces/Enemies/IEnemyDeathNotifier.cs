using System;
using Project.Configs.Enemies;

namespace Project.Interfaces.Enemies
{
    public interface IEnemyDeathNotifier
    {
        event Action<EnemyConfig> EnemyDied;
    }
}