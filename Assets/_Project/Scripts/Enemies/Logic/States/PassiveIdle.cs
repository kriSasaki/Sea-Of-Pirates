using Project.Enemies;
using UnityEngine;

namespace Project.Enemies.Logic.States
{
    [CreateAssetMenu(fileName = "PassiveIdle", menuName = "Configs/Enemies/States/PassiveIdle")]
    public class PassiveIdle : IdleState
    {
        public override void Update()
        {
            base.Update();

            if (Enemy.Position != Enemy.SpawnPosition)
            {
                Enemy.Mover.Move(Enemy.SpawnPosition);
            }
        }
    }
}