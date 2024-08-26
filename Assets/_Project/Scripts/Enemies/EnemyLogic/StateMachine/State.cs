using Project.Players.PlayerLogic;
using UnityEngine;

namespace Project.Enemies.EnemyLogic.StateMachine
{
    public abstract class State
    {
        protected readonly StateMachine _stateMachine;

        protected Player _target;

        protected State(StateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public virtual void Update()
        {
        }
    }
}