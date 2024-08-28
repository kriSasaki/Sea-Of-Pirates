using Project.Configs.Enemies;
using Project.Enemies;
using Project.Players.Logic;
using UnityEngine;

public abstract class BaseState : ScriptableObject
{
    protected Enemy Enemy { get; private set; }
    protected Player Player { get; private set; }
    protected EnemyConfig Config { get; private set; }

    public void Initialize(Enemy enemy, Player player)
    {
        Enemy = enemy;
        Player = player;
        Config = Enemy.Config;

        OnInitialize();
    }
    public virtual void Enter()
    {
    }

    public virtual void Exit()
    {
    }

    public virtual void Update()
    {
    }

    protected virtual void OnInitialize() 
    { 
    }
}
