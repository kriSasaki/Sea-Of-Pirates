﻿using Project.Utils.Extensions;
using UnityEngine;

namespace Project.Players.Logic
{
    public class MobileMoveHandler : MoveHandler
    {
        private readonly Camera _camera;

        private Vector3 _inputDirection;

        public MobileMoveHandler(IInputService inputService)
            : base(inputService)
        {
            _camera = Camera.main;
        }

        private float MoveAngleDot => PlayerMove.MoveAngleDot;


        public override void ReadInput()
        {
            _inputDirection = _camera.transform.TransformDirection(InputService.Axis).WithZeroY();
        }

        public override void Rotate()
        {
            if (_inputDirection == Vector3.zero)
                return;

            Quaternion lookRotation = Quaternion.LookRotation(_inputDirection);
            Rigidbody.rotation = Quaternion.RotateTowards(Rigidbody.rotation, lookRotation, RotationSpeed * Time.deltaTime);
        }

        public override void Move()
        {

            if (Vector3.Dot(Transform.forward, _inputDirection) < MoveAngleDot)
                return;
            Vector3 direction = _inputDirection.magnitude > 1f ? _inputDirection.normalized : _inputDirection;

            Vector3 velocity = (direction * MovementSpeed);

            Rigidbody.velocity = Vector3.MoveTowards(Rigidbody.velocity, velocity, MaxDistanceDelta);
        }
    }
}