using System;
using Project.Utils.Extensions;
using UnityEngine;

namespace Project.Players.Logic
{
    public class DesktopMoveHandler : MoveHandler
    {
        private Vector3 _input;

        public DesktopMoveHandler(IInputService inputService)
            : base(inputService)
        {
        }

        public override void ReadInput()
        {
            _input = InputService.Axis;

            PlayerMove.SetForwardValue(_input.y);
        }

        public override void Rotate()
        {
            if (Math.Abs(_input.x) < 0.01f)
                return;

            float rotationY = _input.x * RotationSpeed;
            Quaternion deltaRotation = Quaternion.Euler(Vector3.zero.WithY(rotationY) * Time.deltaTime);

            Rigidbody.MoveRotation(Rigidbody.rotation * deltaRotation);
        }

        public override void Move()
        {
            float speed = _input.y >= 0f ? MovementSpeed : MovementSpeed * PlayerMove.ReverseMoveMultiplier;
            Vector3 velocity = _input.y * speed * Transform.forward;

            Rigidbody.velocity = Vector3.MoveTowards(Rigidbody.velocity, velocity, MaxDistanceDelta);
        }
    }
}