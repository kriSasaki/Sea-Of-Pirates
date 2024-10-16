using System;
using System.Collections.Generic;
using Scripts.Configs.Enemies;
using Scripts.Enemies.Logic.States;
using Scripts.Enemies.Logic.States.Battle;
using Scripts.Enemies.Logic.States.Idle;
using Scripts.Interfaces.Audio;
using Scripts.Players.Logic;
using UnityEngine;

namespace Scripts.Enemies.Logic
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyStateMachine : MonoBehaviour
    {
        private readonly Dictionary<Type, BaseState> _states = new();

        private Player _player;
        private Enemy _enemy;
        private IAudioService _audioService;

        private BaseState CurrentState { get; set; }

        private void Update()
        {
            CurrentState.Update();
        }

        private void OnDestroy()
        {
            if (CurrentState != null)
                CurrentState.Exit();

            foreach (BaseState state in _states.Values)
            {
                Destroy(state);
            }
        }

        public void Initialize(Player player, IAudioService audioService)
        {
            _enemy = GetComponent<Enemy>();
            _player = player;
            _audioService = audioService;
            EnemyBehaviorConfig config = _enemy.Config.BehaviorConfig;

            RegisterState<IdleState>(config.IdleState);
            RegisterState<BattleState>(config.BattleState);
            RegisterState<DeadState>(config.DeadState);

            SetState<IdleState>();
        }

        public void RegisterState<T>(T state)
            where T : BaseState
        {
            if (!_states.ContainsKey(typeof(T)))
            {
                T newState = Instantiate(state);
                newState.Initialize(_enemy, _player, this, _audioService);

                _states.Add(typeof(T), newState);
            }
            else
            {
                Debug.LogWarning("Уже есть " + typeof(T));
            }
        }

        public void SetState<T>()
            where T : BaseState
        {
            Type type = typeof(T);

            if (_states.ContainsKey(type) == false)
                throw new Exception($"StateMachine has not registered state {typeof(T)}");

            SwitchState(_states[type]);
        }

        private void SwitchState(BaseState state)
        {
            if (CurrentState == state)
                return;

            if (CurrentState != null)
                CurrentState.Exit();

            CurrentState = state;
            CurrentState.Enter();
        }
    }
}