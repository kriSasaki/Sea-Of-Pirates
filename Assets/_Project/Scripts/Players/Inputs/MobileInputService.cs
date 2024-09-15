
using UnityEngine;

namespace Project.Players.Inputs
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => SimpleInputAxis();
    }
}
