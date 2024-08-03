using UnityEngine;

namespace Project.Players.PlayerLogic
{
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private Player Health;
        [SerializeField] private PlayerMove Move;
        [SerializeField] private GameObject DeathEffect;

        private bool _isDead;

        private void Start() => Health.HealthChanged += HealthChanged;

        private void OnDestroy() => Health.HealthChanged -= HealthChanged;

        private void HealthChanged()
        {
            if (!_isDead && Health.CurrentHealth <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            _isDead = true;
            Move.enabled = false;
            Instantiate(DeathEffect, transform.position, Quaternion.identity);
        }
    }
}