using UnityEngine;

namespace Scripts.Players.Inputs
{
    public class InputService : IInputService
    {
        private const string Horizontal = "Horizontal";
        private const string Vertical = "Vertical";

        public Vector2 Axis => new Vector2(SimpleInput.GetAxis(Horizontal), SimpleInput.GetAxis(Vertical));
    }
}