using Project.Configs.Enemies;
using Project.Enemies;
using Project.Interfaces.Enemies;
using Project.Players.Logic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyStateMachine : MonoBehaviour
{
    private IdleState _idleState;
    private BattleState _battleState;
    private DeadState _deadState;

    private Player _player;
    private Enemy _enemy;

    private BaseState CurrentState { get; set; }

    private void Update()
    {
        CurrentState.Update();
    }

    private void OnDestroy()
    {
        CurrentState.Exit();

        _enemy.Died -= OnEnemyDied;
        _enemy.Respawned -= OnEnemyRespawned;
        _enemy.PlayerDetected -= OnPlayerDetected;
        _enemy.PlayerLost -= OnPlayerLost;

        Destroy(_idleState);
        Destroy(_battleState);
        Destroy(_deadState);
    }

    public void Initialize(Player player)
    {
        _enemy = GetComponent<Enemy>();
        _player = player;
        EnemyBehaviorConfig config = _enemy.Config.BehaviourConfig;

        _idleState = Instantiate(config.IdleState);
        _battleState = Instantiate(config.BattleState);
        _deadState = Instantiate(config.DeadState);


        _idleState.Initialize(_enemy, _player);
        _battleState.Initialize(_enemy, _player);
        _deadState.Initialize(_enemy, _player);

        _enemy.Died += OnEnemyDied;
        _enemy.Respawned += OnEnemyRespawned;
        _enemy.PlayerDetected += OnPlayerDetected;
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

    private void OnPlayerDetected()
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
}
