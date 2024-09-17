using System;
using System.ComponentModel;
using Project.Interfaces.Stats;
using Project.Players.Inputs;
using Project.Utils.Extensions;
using SimpleInputNamespace;
using UnityEngine;
using Zenject;
using static UnityEditor.Experimental.GraphView.GraphView;

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
        private bool _device;
        private Vector3 _inputDirection;
        private IPlayerStats _playerStats;
        private Joystick _joystick;
        private Player _player;

        private int MovementSpeed => _playerStats.Speed;

        private void Start()
        {
            _camera = Camera.main;     

            if (Agava.WebUtility.Device.IsMobile)
            {
                _device = true;
            }
            else
            {   
                _device = false;
                _joystick = FindObjectOfType<Joystick>();
                _joystick.enabled = false;
            }
        }

        private void Update()
        {
            ReadInput();
        }

        private void FixedUpdate()
        {
            if (_device)
            {
                if (_player.IsAlive == false || _player.CanMove == false)
                    return;

                ControlDivice();
            }
            else
            {
                ControlDesctop();
            }
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
            if (_inputService.Axis.sqrMagnitude > 0.001f)
            {
                _inputDirection = _camera.transform.TransformDirection(_inputService.Axis).WithZeroY();
                _inputDirection.Normalize();
            }
        }

        private void ControlDesctop()
        {
            RotateDesctop();
            MoveDesctop();
           
        }

        private void RotateDesctop()
        {
            float rotationInput = _inputService.Axis.x;
            if (Math.Abs(rotationInput) < 0.1f)
                return;

            float rotationAmount = rotationInput * _rotationSpeed * Time.deltaTime;
            transform.Rotate(0, rotationAmount, 0);
        }

        private void MoveDesctop()
        {
            if (_inputService.Axis.y > 0)
            {
                Vector3 forwardMovement = transform.forward * MovementSpeed * Time.fixedDeltaTime;
                _playerRigidbody.MovePosition(_playerRigidbody.position + forwardMovement);
            }
        }

        private void ControlDivice()
        {
            MoveDivice();
            RotateDivice();
        }

        private void RotateDivice()
        {
            if (_inputDirection == Vector3.zero)
                return;

            Quaternion lookRotation = Quaternion.LookRotation(_inputDirection);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, _rotationSpeed * Time.deltaTime);
        }

        private void MoveDivice()
        {
            Vector3 direction = _inputDirection.magnitude > 1f ? _inputDirection.normalized : _inputDirection;

            if (Vector3.Dot(transform.forward, _inputDirection) < _moveAngleDot)
                direction = Vector3.zero;

            Vector3 velocity = (direction * MovementSpeed);

            _playerRigidbody.velocity = Vector3.MoveTowards(_playerRigidbody.velocity, velocity, MaxDistanceDelta);
        }
    }
}