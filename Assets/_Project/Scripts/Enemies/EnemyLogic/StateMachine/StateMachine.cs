using System;
using System.Collections.Generic;

namespace Project.Enemies.EnemyLogic.StateMachine
{
    public class StateMachine
    {
        private readonly Dictionary<Type, State> _states = new Dictionary<Type, State>();
        
        private State CurrentState { get; set; }

        public void AddState(State state)
        {
            _states.Add(state.GetType(), state);
        }

        public void SetState<T>() where T : State
        {
            var type = typeof(T);

            if (CurrentState != null && CurrentState.GetType() == type)
            {
                return;
            }

            if (_states.TryGetValue(type, out var newState))
            {
                CurrentState = newState;
            }
        }

        public void Update()
        {
            CurrentState?.Update();
        }
    }
}