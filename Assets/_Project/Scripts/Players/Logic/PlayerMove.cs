using System;
using Project.Interfaces.Stats;
using Project.Players.CamaraLogic;
using Project.Utils.Extensions;
using UnityEngine;
using Zenject;

namespace Project.Players.Logic
{
    public class PlayerMove : MonoBehaviour
    {
        private const float MaxDistanceDelta = 0.8f;

        [SerializeField] private Rigidbody _playerRigidbody;
        [SerializeField, Range(30f, 120f)] private float _rotationSpeed;
        [SerializeField, Range(0.1f, 0.7f)] private float _moveAngleDot;

        private IInputService _inputService;
        private Camera _camera;
        private Player _player;
        private IPlayerStats _playerStats;
        private Vector3 _inputDirection;

        private int MovementSpeed => _playerStats.Speed;

        private void Start()
        {
            _camera = Camera.main;
        }

        private void Update()
        {
            ReadInput();
        }

        private void FixedUpdate()
        {
            if (_player.IsAlive == false || _player.CanMove == false)
                return;

            Rotate();
            Move();
        }

        [Inject]
        private void Construct(IPlayerStats playerStats, IInputService inputService, Player player)
        {
            _playerStats = playerStats;
            _inputService = inputService;
            _player = player;
        }

        private void ReadInput()
        {
            _inputDirection = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > 0.001f)
            {
                _inputDirection = _camera.transform.TransformDirection(_inputService.Axis).WithZeroY();
            }
        }

        private void Rotate()
        {
            if (_inputDirection == Vector3.zero)
                return;

            Quaternion lookRotation = Quaternion.LookRotation(_inputDirection);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, _rotationSpeed * Time.fixedDeltaTime);
        }

        private void Move()
        {
            Vector3 direction = _inputDirection.magnitude > 1f ? _inputDirection.normalized : _inputDirection;

            if (Vector3.Dot(transform.forward, _inputDirection) < _moveAngleDot)
                direction = Vector3.zero;

            Vector3 velocity = (direction * MovementSpeed);

            _playerRigidbody.velocity = Vector3.MoveTowards(_playerRigidbody.velocity, velocity, MaxDistanceDelta);
        }
    }
}