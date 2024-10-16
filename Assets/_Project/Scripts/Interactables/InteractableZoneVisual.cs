using DG.Tweening;
using NaughtyAttributes;
using Scripts.Players.Logic;
using UnityEngine;

namespace Scripts.Interactables
{
    public class InteractableZoneVisual : MonoBehaviour
    {
        private const float MinScale = 0;
        private const float ScaleMultiplier = 2f;

        [SerializeField] private InteractableZone _interactableZone;
        [HorizontalLine(3f, EColor.Green)]
        [SerializeField] private MeshRenderer _zoneRenderer;
        [SerializeField] private Color _zoneColor;
        [SerializeField] private float _scaleDuration = 0.5f;
        [SerializeField] private Ease _ease = Ease.InOutSine;

        private float _maxScale;

        private void Start()
        {
            _maxScale = _interactableZone.TriggerZoneRadius * ScaleMultiplier;
            _zoneRenderer.material.color = _zoneColor;
            ShowZone();
        }

        private void OnEnable()
        {
            _interactableZone.PlayerEntered += OnPlayerEntered;
            _interactableZone.PlayerExited += OnPlayerExited;
        }

        private void OnDisable()
        {
            _interactableZone.PlayerEntered -= OnPlayerEntered;
            _interactableZone.PlayerExited -= OnPlayerExited;
        }

        private void ShowZone()
        {
            transform.DOScale(_maxScale, _scaleDuration).SetEase(_ease);
        }

        private void HideZone()
        {
            transform.DOScale(MinScale, _scaleDuration).SetEase(_ease);
        }

        private void OnPlayerEntered(Player player)
            => HideZone();

        private void OnPlayerExited(Player player)
            => ShowZone();
    }
}