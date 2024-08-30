using Project.Enemies.Logic.States.Idle;
using UnityEngine;

namespace Project.Enemies.Logic.States
{
    [CreateAssetMenu(fileName = "Dead", menuName = "Configs/Enemies/States/Dead")]
    public class DeadState : BaseState
    {
        public override void Enter()
        {
            base.Enter();
            Enemy.Respawned += OnEnemyRespawned;
            Enemy.Detector.Disable();
        }

        private void OnEnemyRespawned()
        {
            StateMachine.SetState<IdleState>();
        }

        public override void Exit()
        {
            base.Exit();
            Enemy.Respawned -= OnEnemyRespawned;
        }
    }
}