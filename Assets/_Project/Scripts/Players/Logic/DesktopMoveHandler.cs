using System;
using Scripts.Utils.Extensions;
using UnityEngine;

namespace Scripts.Players.Logic
{
    public class DesktopMoveHandler : MoveHandler
    {
        private const float InputTrashhold = 0.01f;
        private const float Zero = 0f;
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
            if (Math.Abs(_input.x) < InputTrashhold)
                return;

            float rotationY = _input.x * RotationSpeed;
            Quaternion deltaRotation = Quaternion.Euler(Vector3.zero.WithY(rotationY) * Time.deltaTime);

            Rigidbody.MoveRotation(Rigidbody.rotation * deltaRotation);
        }

        public override void Move()
        {
            float speed = _input.y >= Zero ? MovementSpeed : MovementSpeed * PlayerMove.ReverseMoveMultiplier;
            Vector3 velocity = _input.y * speed * Transform.forward;

            Rigidbody.velocity = Vector3.MoveTowards(Rigidbody.velocity, velocity, MaxDistanceDelta);
        }
    }
}