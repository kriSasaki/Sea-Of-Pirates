using System;
using Scripts.Players.Logic;
using UnityEngine;

namespace Scripts.Enemies.Logic
{
    [RequireComponent(typeof(SphereCollider))]
    public class PlayerDetector : MonoBehaviour
    {
        [SerializeField] private float _detectRange = 1f;

        private SphereCollider _detectZone;

        public event Action PlayerDetected;
        public event Action PlayerLost;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
                PlayerDetected?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
                PlayerLost?.Invoke();
        }

        public void Initialize(float detectRange)
        {
            _detectZone = GetComponent<SphereCollider>();
            _detectZone.radius = detectRange;
            _detectRange = detectRange;
        }

        public void Enable()
            => _detectZone.enabled = true;

        public void Disable()
            => _detectZone.enabled = false;

        public void CheckPlayer()
        {
            Disable();
            Enable();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _detectRange);
        }
    }
}