using UnityEngine;
using Scripts.Players.Inputs.InputServices;

namespace Scripts.Players.Inputs.MobileInputService
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => SimpleInputAxis();
    }
}