using Scripts.Configs.Enemies;
using Scripts.Interfaces.Audio;
using Scripts.Players.Logic;
using UnityEngine;

namespace Scripts.Enemies.Logic.States
{
    public abstract class BaseState : ScriptableObject
    {
        protected Enemy Enemy { get; private set; }
        protected Player Player { get; private set; }
        protected EnemyConfig Config { get; private set; }
        protected EnemyStateMachine StateMachine { get; private set; }
        protected IAudioService AudioService { get; private set; }

        public void Initialize(
            Enemy enemy,
            Player player,
            EnemyStateMachine stateMachine,
            IAudioService audioService)
        {
            Enemy = enemy;
            Player = player;
            Config = Enemy.Config;
            AudioService = audioService;
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