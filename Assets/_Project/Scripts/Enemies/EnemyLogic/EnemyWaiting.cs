using System;
using UnityEngine;

namespace Project.Enemies.EnemyLogic
{
    public class EnemyWaiting : MonoBehaviour
    {
        [SerializeField] private EnemyMove _enemyMove;
        [SerializeField] private EnemyChase _enemyChase;
        [SerializeField] private EnemyAttack _enemyAttack;

        private Vector3 _spawnPosition;
        private float _distanceFromSpawn;
        
        private void Awake()
        {
            _spawnPosition = transform.position;
            enabled = false;
        }

        private void Update()
        {
            if (transform.position == _spawnPosition)
            {
                enabled = false;
            }
            
            _enemyMove.Move(_spawnPosition);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _enemyChase.StartChase(player);
                _enemyAttack.SetTarget(player);
                enabled = false;
            }
        }

        public void StartWaiting()
        {
            enabled = true;
        }
    }
}