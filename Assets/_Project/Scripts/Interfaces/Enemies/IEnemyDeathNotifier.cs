using System;
using Scripts.Configs.Enemies;

namespace Scripts.Interfaces.Enemies
{
    public interface IEnemyDeathNotifier
    {
        event Action<EnemyConfig> EnemyDied;
    }
}