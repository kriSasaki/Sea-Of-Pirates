using System.Collections;
using UnityEngine;

namespace Project.Players.PlayerLogic
{
    public class PlayerDeath : MonoBehaviour
    {
        [SerializeField] private Player _health;
        [SerializeField] private PlayerMove _move;
        [SerializeField] private GameObject _deathEffect;
        [SerializeField] private Transform _respawnPoint;

        private float _effectTime = 3f;
        private bool _isDead;

        private void Start() => _health.HealthChanged += OnHealthChanged;

        private void OnDestroy() => _health.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged()
        {
            if (!_isDead && _health.CurrentHealth <= 0)
            {
                StartCoroutine(Die());
            }
        }

        private IEnumerator Die()
        {
            _isDead = true;
            _move.enabled = true;
            _deathEffect.SetActive(true);

            yield return new WaitForSeconds(_effectTime);

            transform.position = _respawnPoint.transform.position;
            _deathEffect.SetActive(false);
            _move.enabled = true;
        }
    }
}