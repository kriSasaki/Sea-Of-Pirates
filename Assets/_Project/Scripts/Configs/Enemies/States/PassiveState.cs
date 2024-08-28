using Project.Enemies;
using UnityEngine;

[CreateAssetMenu]
public class PassiveState : IdleState
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