using UnityEngine;

namespace Scripts.Enemies
{
    public struct ProjectileSettings
    {
        public float Radius;
        public float ExplodeDelay;
        public LayerMask TargetMask;

        public ProjectileSettings(float radius, float explodeDelay, LayerMask targetMask)
        {
            Radius = radius;
            ExplodeDelay = explodeDelay;
            TargetMask = targetMask;
        }
    }
}