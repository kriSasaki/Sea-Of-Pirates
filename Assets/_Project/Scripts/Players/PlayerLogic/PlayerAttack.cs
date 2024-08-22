using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Project.Enemies;
using Project.Players.Inputs;
using Zenject;
using Project.Interfaces.Stats;

namespace Project.Players.PlayerLogic
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private SphereCollider _attackZone;

        private IPlayerStats _playerStats;

        public int Damage => _playerStats.Damage;
        public int AttackRange => _playerStats.AttackRange;
        public int CannonsAmount => _playerStats.CannonsAmount;

        private void Start()
        {


            SetAttackZone();
        }

        private void OnTriggerEnter(Collider other)
        {
            
        }

        private void SetAttackZone()
        {
            _attackZone.radius = AttackRange;
        }

        [Inject]
        public void Construct(IPlayerStats playerStats)
        {
            _playerStats = playerStats;
        }


    }
}
