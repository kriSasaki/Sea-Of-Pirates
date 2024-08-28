using DG.Tweening;
using Project.Enemies;
using Project.Utils.Extensions;
using System.Collections;
using UnityEngine;

[CreateAssetMenu]
public class WanderingState : IdleState
{
    [SerializeField] private float _lookAtDuration = 0.5f;
    [SerializeField] private float _movementRange = 5f;

    private Vector3 _startPosition;
    private Vector3 _nextPosition;

    private Coroutine _waitCoroutine;
    public override void Enter()
    {
        base.Enter();
        _startPosition = Enemy.Position;
        _nextPosition = (Random.insideUnitSphere * _movementRange + _startPosition).WithY(Enemy.Position.y);
    }

    public override void Exit()
    {
        base.Exit();
        Enemy.transform.DOKill();

        if (_waitCoroutine != null)
            Enemy.StopCoroutine(_waitCoroutine);
    }

    public override void Update()
    {
        base.Update();

        Enemy.Mover.Move(_nextPosition);

        if (Vector3.Distance(Enemy.Position, _nextPosition) <0.01f)
        {
            _nextPosition = (Random.insideUnitSphere * _movementRange + _startPosition).WithY(Enemy.Position.y);
            Debug.Log("new pos is" + _nextPosition + Enemy.name);
        }
    }

    private void StartMoving()
    {
        Vector3 nextPosition = (Random.insideUnitSphere * _movementRange + _startPosition).WithY(Enemy.Position.y);

        Enemy.transform.DOLookAt(nextPosition, _lookAtDuration);

        Enemy.transform.DOMove(nextPosition, Config.Speed, false)
            .SetSpeedBased()
            .OnComplete(() => _waitCoroutine = Enemy.StartCoroutine(WaitForMove()));
    }

    private IEnumerator WaitForMove()
    {
        float moveInterval = Random.value;

        yield return new WaitForSeconds(moveInterval);

        StartMoving();
    }
}
