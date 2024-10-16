using UnityEngine;

namespace Scripts.Players.Logic
{
    public abstract class MoveHandler
    {
        protected const float MaxDistanceDelta = 0.8f;

        public MoveHandler(IInputService inputService)
        {
            InputService = inputService;
        }

        protected IInputService InputService { get; private set; }
        protected Rigidbody Rigidbody { get; private set; }
        protected Transform Transform { get; private set; }
        protected PlayerMove PlayerMove { get; private set; }
        protected float RotationSpeed => PlayerMove.RotationSpeed;
        protected float MovementSpeed => PlayerMove.MovementSpeed;

        public virtual void Initialize(Rigidbody rigidbody, PlayerMove playerMove)
        {
            Rigidbody = rigidbody;
            Transform = rigidbody.transform;
            PlayerMove = playerMove;
        }

        public abstract void ReadInput();

        public abstract void Move();

        public abstract void Rotate();
    }
}