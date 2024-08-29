using Project.Configs.Enemies;
using Project.Enemies.Logic;
using Project.Players.Logic;
using UnityEngine;

namespace Project.Enemies.Logic.States
{
    public abstract class BaseState : ScriptableObject
    {
        protected Enemy Enemy { get; private set; }
        protected Player Player { get; private set; }
        protected EnemyConfig Config { get; private set; }
        protected EnemyStateMachine StateMachine { get; private set; }

        public void Initialize(Enemy enemy, Player player, EnemyStateMachine stateMachine)
        {
            Enemy = enemy;
            Player = player;
            Config = Enemy.Config;
            StateMachine = stateMachine;

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
}