using System;
using Project.Players.Logic;
using UnityEngine;

namespace Project.Enemies
{
    [RequireComponent(typeof(SphereCollider))]
    public class PlayerDetector : MonoBehaviour
    {
        [SerializeField] private float _detectRange = 1f;

        private SphereCollider _detectZone;

        public event Action PlayerDetected;
        public event Action PlayerLost;

        public void Initialize(float detectRange)
        {
            _detectZone = GetComponent<SphereCollider>();
            _detectZone.radius = detectRange;
            _detectRange = detectRange;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Player>(out Player player))
                PlayerDetected?.Invoke();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<Player>(out Player player))
                PlayerLost?.Invoke();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.gray;
            Gizmos.DrawWireSphere(transform.position, _detectRange);
        }
    }
}