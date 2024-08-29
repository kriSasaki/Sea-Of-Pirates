using Project.Configs.Enemies;
using Project.Enemies.Logic.States;
using Project.Interfaces.Enemies;
using Project.Players.Logic;
using UnityEngine;

namespace Project.Enemies.Logic
{
    [RequireComponent(typeof(Enemy))]
    public class EnemyStateMachine : MonoBehaviour
    {
        public IdleState _idleState;
        public BattleState _battleState;
        public DeadState _deadState;

        private Player _player;
        private Enemy _enemy;

        private BaseState CurrentState { get; set; }

        private void Update()
        {
            CurrentState.Update();
        }

        private void OnDestroy()
        {
            if (CurrentState != null)
                CurrentState.Exit();

            Destroy(_idleState);
            Destroy(_battleState);
            Destroy(_deadState);
        }

        public void Initialize(Player player)
        {
            _enemy = GetComponent<Enemy>();
            _player = player;
            EnemyBehaviorConfig config = _enemy.Config.BehaviorConfig;

            _idleState = Instantiate(config.IdleState);
            _battleState = Instantiate(config.BattleState);
            _deadState = Instantiate(config.DeadState);


            _idleState.Initialize(_enemy, _player, this);
            _battleState.Initialize(_enemy, _player, this);
            _deadState.Initialize(_enemy, _player, this);

            SetState(_idleState);
        }

        public void SetState(BaseState state)
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