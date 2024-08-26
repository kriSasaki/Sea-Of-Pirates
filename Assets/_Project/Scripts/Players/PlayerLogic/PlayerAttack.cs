using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Project.Interfaces.Stats;

namespace Project.Players.PlayerLogic
{
    public class PlayerAttack : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _gunList;
        private IPlayerStats _playerStats;

        public int Damage => _playerStats.Damage;
        public int AttackRange => _playerStats.AttackRange;
        private int _cannonsAmount => _playerStats.CannonsAmount;

        private void Start()
        {
            for (int i = 0; i < _cannonsAmount; i++)
            {
                _gunList[i].SetActive(true);
            }
        }

        [Inject]
        public void Construct(IPlayerStats playerStats)
        {
            _playerStats = playerStats;
        }
    }
}
