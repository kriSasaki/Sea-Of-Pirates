using System;
using Project.Interfaces.Stats;
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
        [SerializeField] private float _moveSpeed;
        private IInputService _inputService;
        private Camera _camera;

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
            Move();
            Rotate();
        }


        [Inject]
        private void Construct(IInputService inputService)
        {
            _inputService = inputService;
        }

        private void ReadInput()
        {
            if (_inputService.Axis.sqrMagnitude > 0.001f)
            {
                Vector3 inputDirection = _camera.transform.TransformDirection(_inputService.Axis).WithZeroY();
                inputDirection.Normalize();
            }
        }

        private void Rotate()
        {
            float rotationInput = _inputService.Axis.x;
            if (Math.Abs(rotationInput) < 0.1f)
                return;

            float rotationAmount = rotationInput * _rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotationAmount, 0);
        }

        private void Move()
        {
            if (_inputService.Axis.y > 0)
            {
                Vector3 forwardMovement = transform.forward * _moveSpeed * Time.fixedDeltaTime;
                _playerRigidbody.MovePosition(_playerRigidbody.position + forwardMovement);
            }
        }
    }
}