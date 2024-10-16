using System;
using Scripts.Players.Logic;
using UnityEngine;

namespace Scripts.Interactables
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class InteractableZone : MonoBehaviour
    {
        [SerializeField] private float _triggerZoneRadius = 30f;
        [SerializeField] private Color _gizmosColor = Color.white;

        private SphereCollider _triggerZone;

        public event Action<Player> PlayerEntered;
        public event Action<Player> PlayerExited;

        public float TriggerZoneRadius => _triggerZoneRadius;

        protected void Awake()
        {
            _triggerZone = GetComponent<SphereCollider>();
            _triggerZone.isTrigger = true;
            _triggerZone.radius = _triggerZoneRadius;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                PlayerEntered?.Invoke(player);
                OnPlayerEntered(player);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                PlayerExited?.Invoke(player);
                OnPlayerExited(player);
            }
        }

        protected virtual void OnPlayerEntered(Player player)
        {
        }

        protected virtual void OnPlayerExited(Player player)
        {
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _gizmosColor;
            Gizmos.DrawWireSphere(transform.position, _triggerZoneRadius);
        }
    }
}