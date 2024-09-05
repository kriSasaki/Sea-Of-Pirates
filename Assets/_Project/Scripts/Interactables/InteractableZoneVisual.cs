using System;
using DG.Tweening;
using Project.Players.Logic;
using UnityEngine;

namespace Project.Interactables
{
    public class InteractableZoneVisual : InteractableZone
    {
        private const float MinScale = 0;

        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _zoneColor;
        [SerializeField] private float ScaleDuration = 0.5f;

        private float _maxScale;
        
        private void Start()
        {
            _maxScale = TriggerZone.radius;
            _spriteRenderer.gameObject.transform.localScale =
                new Vector3(_maxScale, _maxScale, _maxScale);
            _spriteRenderer.color = _zoneColor;
        }

        private void Show()
        {
            _spriteRenderer.transform.DOScale(_maxScale, ScaleDuration).SetEase(Ease.InOutSine);
        }

        private void Hide()
        {
            _spriteRenderer.transform.DOScale(MinScale, ScaleDuration).SetEase(Ease.InOutSine);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
               Hide();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                Show();
            }
        }
    }
}