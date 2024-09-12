using UnityEngine;
namespace Project.Players.Inputs
{
    public class StandaloneInputService : InputService
    {
        private Vector2 _currentAxis = Vector2.zero;

        public override Vector2 Axis
        {
            get
            {
                Vector2 targetAxis = SimpleInputAxis();

                if (targetAxis == Vector2.zero)
                {
                    targetAxis = UnityAxis();
                }

                return SmoothTransition(targetAxis);
            }
        }

     
        public override bool IsMovingForward => Input.GetKey(KeyCode.W);

        private static Vector2 UnityAxis() =>
            new Vector2(Input.GetAxis("Horizontal"), 0);

        private Vector2 SmoothTransition(Vector2 targetAxis)
        {
            float smoothTime = 0.1f;
            _currentAxis = Vector2.Lerp(_currentAxis, targetAxis, Time.deltaTime / smoothTime);
            return _currentAxis;
        }
    }
}