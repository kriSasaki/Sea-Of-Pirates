using Project.Configs.Enemies;
using Project.Enemies;
using Project.Interfaces.Enemies;
using Project.Players.Logic;
using System;
using UnityEngine;

namespace Project.Configs.Enemies
{
    [CreateAssetMenu(fileName = "EnemyBehaviour", menuName = "Configs/Enemies/Behaviour")]
    public class EnemyBehaviourConfig : ScriptableObject
    {
        [field: SerializeField] public IdleState IdleState { get; private set; }
        [field: SerializeField] public BattleState BattleState { get; private set; }
        [field: SerializeField] public DeadState DeadState { get; private set; }
    }
}


public class EnemyStateMachine : IDisposable
{
    private readonly IdleState _idleState;
    private readonly BattleState _battleState;
    private readonly DeadState _deadState;
    private readonly Player _player;

    private Enemy _enemy;

    private BaseState CurrentState { get; set; }

    public EnemyStateMachine(EnemyBehaviourConfig config, Player player)
    {
        _idleState = GameObject.Instantiate(config.IdleState);
        _battleState = GameObject.Instantiate(config.BattleState);
        _deadState = GameObject.Instantiate(config.DeadState);
        _player = player;
    }

    public void Initialize(Enemy enemy)
    {
        _enemy = enemy;

        _idleState.Initialize(enemy, _player);
        _battleState.Initialize(enemy, _player);
        _deadState.Initialize(enemy, _player);

        _enemy.Died += OnEnemyDied;
        _enemy.Respawned += OnEnemyRespawned;
        _enemy.PlayerDetected += OnPlayerDeteceted;
        _enemy.PlayerLost += OnPlayerLost;

        SetState(_idleState);
    }

    private void OnPlayerLost()
    {
        if (CurrentState != _deadState)
            SetState(_idleState);
    }

    private void OnEnemyRespawned()
    {
        SetState(_idleState);
    }

    private void OnPlayerDeteceted()
    {
        SetState(_battleState);
    }

    private void OnEnemyDied(IEnemy enemy)
    {
        SetState(_deadState);
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

    public void Update()
    {
        CurrentState.Update();
    }

    public void Dispose()
    {
        CurrentState.Exit();

        _enemy.Died -= OnEnemyDied;
        _enemy.Respawned -= OnEnemyRespawned;
        _enemy.PlayerDetected -= OnPlayerDeteceted;
        _enemy.PlayerLost -= OnPlayerLost;

        GameObject.Destroy(_idleState);
        GameObject.Destroy(_battleState);
        GameObject.Destroy(_deadState);
    }
}
