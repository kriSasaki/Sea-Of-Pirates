using System;
using Project.Enemies.EnemyLogic.StateMachine;
using UnityEngine;

namespace Project.Enemies.EnemyLogic
{
    public class EnemyWaiting : EnemyMove
    {
        private readonly Vector3 SpawnPosition;

        private float DistanceFromSpawn;

        public EnemyWaiting(StateMachine.StateMachine stateMachine, Vector3 spawnPosition, Transform transform,
            float speed, float rotateSpeed, float maxMagnitudeDelta) : base(stateMachine, transform, speed, rotateSpeed,
            maxMagnitudeDelta)
        {
            SpawnPosition = spawnPosition;
            Transform = transform;
        }

        public override void Update()
        {
            if (Transform.position != SpawnPosition)
            {
                Move(SpawnPosition);
            }
        }
    }
}