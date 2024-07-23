using UnityEngine;

namespace Scripts.Players.PlayerLogic
{
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private PlayerHealth Health;
        [SerializeField] private PlayerMove Move;
        [SerializeField] private GameObject DeathEffect;

        private bool _isDead;

        private void Start() => Health.HealthChange += HealthChanged;

        private void OnDestroy() => Health.HealthChange -= HealthChanged;

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