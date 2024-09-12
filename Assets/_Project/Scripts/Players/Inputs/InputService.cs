using UnityEngine;
namespace Project.Players.Inputs
{
    public abstract class InputService : IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        public abstract Vector2 Axis { get; }
        public abstract bool IsMovingForward { get; }

        protected static Vector2 SimpleInputAxis() =>
            new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}