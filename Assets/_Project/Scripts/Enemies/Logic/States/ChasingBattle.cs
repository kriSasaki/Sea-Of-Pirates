using Cysharp.Threading.Tasks;
using DG.Tweening;
using Project.Players.Logic;
using System.Threading;
using UnityEngine;

namespace Project.Enemies.Logic.States
{
    [CreateAssetMenu(fileName = "ChasingBattle", menuName = "Configs/Enemies/States/ChasingBattle")]
    public class ChasingBattle : BattleState
    {
        [SerializeField] private float _maxDistanceFromSpawn;

        private bool _isAttacking;

        public override void Enter()
        {
            base.Enter();

            _isAttacking = false;
        }

        public override void Exit()
        {
            base.Exit();

            _isAttacking = false;
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
                    Attack(ExitToken).Forget();
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
            while (Vector3.Distance(Player.transform.position, Enemy.Position) < Config.AttackRange)
            {
                Vector3 direction = Player.transform.position - Enemy.Position;
                Quaternion rotation = Quaternion.FromToRotation(Enemy.transform.forward, direction);

                await UniTask.WhenAll(
                    Enemy.transform.DORotate(rotation.eulerAngles, Config.RotationSpeed).SetSpeedBased().WithCancellation(token),
                    UniTask.Delay(System.TimeSpan.FromSeconds(Config.AttackICooldown), cancellationToken: token));

                float distanceFromTarget = Vector3.Distance(Player.transform.position, Enemy.Position);

                if (distanceFromTarget <= Config.AttackRange)
                    Enemy.DealDamage(Player);
            }

            _isAttacking = false;
        }
    }
}