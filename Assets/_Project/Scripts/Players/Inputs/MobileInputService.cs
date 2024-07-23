using UnityEngine;

namespace Scripts.Players.Inputs
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => SimpleInputAxis();
    }
}