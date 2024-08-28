using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.Enemies;
using Project.Players.Logic;
using System.Threading;
using UnityEngine;

[CreateAssetMenu]
public class ChasingState : BattleState
{
    [SerializeField] private float _maxDistanceFromSpawn;

    private bool _isAttacking;
    private CancellationTokenSource _exitCancellation;

    private void OnDestroy()
    {
        _exitCancellation?.Cancel();
        _exitCancellation?.Dispose();
    }

    public override void Enter()
    {
        base.Enter();

        _exitCancellation?.Dispose();
        _exitCancellation = new();

        _isAttacking = false;
    }

    public override void Exit()
    {
        base.Exit();

        _isAttacking = false;
        _exitCancellation.Cancel();
    }

    public override void Update()
    {
        base.Update();

        if (_isAttacking)
            return;

        float distanceFromTarget = Vector3.Distance(Player.transform.position, Enemy.Position);
        float distanceFromSpawn = Vector3.Distance(Enemy.SpawnPosition, Enemy.Position);


        if (distanceFromSpawn <= _maxDistanceFromSpawn)
        {
            if (distanceFromTarget <= Config.AttackRange)
            {
                Attack(_exitCancellation.Token).Forget();
            }
            else
            {
                Enemy.Mover.Move(Player.transform.position);
            }
        }
        else
        {
            Enemy.Mover.Move(Enemy.SpawnPosition);
        }
    }

    private async UniTaskVoid Attack(CancellationToken token)
    {
        _isAttacking = true;

        Vector3 direction = Player.transform.position - Enemy.Position;
        Quaternion rotation = Quaternion.FromToRotation(Enemy.transform.forward, direction);

        await UniTask.WhenAll(
            Enemy.transform.DORotate(rotation.eulerAngles, 1f).WithCancellation(token),
            UniTask.Delay(System.TimeSpan.FromSeconds(Config.AttackInterval), cancellationToken: token));

        float distanceFromTarget = Vector3.Distance(Player.transform.position, Enemy.Position);

        if (distanceFromTarget <= Config.AttackRange)
            Player.TakeDamage(Config.Damage);

        _isAttacking = false;
    }
}