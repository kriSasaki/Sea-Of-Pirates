using DG.Tweening;
using UnityEngine;

namespace Project.Interactables
{
    public class InteractableZoneVisual : MonoBehaviour
    {
        private const float MinScale = 0;

        [SerializeField] private InteractableZone _interactableZone;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Color _zoneColor;
        [SerializeField] private float ScaleDuration = 0.5f;

        private float _maxScale;
        
        private void Start()
        {
            _maxScale = _interactableZone.TriggerZone;
            _spriteRenderer.gameObject.transform.localScale =
                new Vector3(_maxScale, _maxScale, _maxScale);
            _spriteRenderer.color = _zoneColor;
        }

        private void OnEnable()
        {
            _interactableZone.PlayerEntered += Hide;
            _interactableZone.PlayerCameOut += Show;
        }

        private void OnDisable()
        {
            _interactableZone.PlayerEntered -= Hide;
            _interactableZone.PlayerCameOut -= Show;
        }

        private void Show()
        {
            _spriteRenderer.transform.DOScale(_maxScale, ScaleDuration).SetEase(Ease.InOutSine);
        }

        private void Hide()
        {
            _spriteRenderer.transform.DOScale(MinScale, ScaleDuration).SetEase(Ease.InOutSine);
        }
    }
}