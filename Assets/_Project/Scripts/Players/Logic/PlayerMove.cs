using System;
using Project.Interfaces.Stats;
using Project.Players.Inputs;
using SimpleInputNamespace;
using UnityEngine;
using UnityEngine.XR;
using Zenject;


namespace Project.Players.Logic
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody _playerRigidbody;
        [SerializeField, Range(30f, 120f)] private float _rotationSpeed;
        [SerializeField, Range(0.1f, 0.7f)] private float _moveAngleDot;
        [SerializeField, Range(0.1f,1f)] private float _reverseMoveMultiplier;

        private IPlayerStats _playerStats;
        private Player _player;
        private MoveHandler _moveHandler;

        public int MovementSpeed => _playerStats.Speed;
        public float RotationSpeed => _rotationSpeed;
        public float MoveAngleDot => _moveAngleDot;
        public float ReverseMoveMultiplier => _reverseMoveMultiplier;

        private void Update()
        {
            if (_player.IsAlive == false || _player.CanMove == false)
                return;

            _moveHandler.ReadInput();
        }

        private void FixedUpdate()
        {
            if (_player.IsAlive == false || _player.CanMove == false)
                return;

            _moveHandler.Rotate();
            _moveHandler.Move();
        }

        [Inject]
        private void Construct(IPlayerStats playerStats, Player player, MoveHandler moveHandler)
        {
            _playerStats = playerStats;
            _player = player;
            _moveHandler = moveHandler;

            _moveHandler.Initialize(_playerRigidbody,this);
        }
    }
}