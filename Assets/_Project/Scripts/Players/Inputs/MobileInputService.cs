    using UnityEngine;
    namespace Project.Players.Inputs
    {
        public class MobileInputService : InputService
        {
            public override Vector2 Axis
            {
                get
                {
                    if (Input.anyKey) return Vector2.zero;
                    return SimpleInputAxis();
                }
            }

            public override bool IsMovingForward => SimpleInput.GetAxis("Vertical") > 0;
        }
    }
