using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Scripts.Utils.Tweens
{
    [System.Serializable]
    public class AppearingUITween
    {
        [SerializeField] private float _duration = 0.4f;
        [SerializeField] private Ease _ease = Ease.Linear;
        [SerializeField] private Vector2 _hidedAnchorPosition;

        private RectTransform _rectTransform;
        private Vector2 _originPosition;

        public void Initialize(RectTransform rectTransform)
        {
            _rectTransform = rectTransform;
            _originPosition = _rectTransform.anchoredPosition;
            _rectTransform.anchoredPosition = _hidedAnchorPosition;
        }

        public void Appear()
        {
            _rectTransform.DOAnchorPos(_originPosition, _duration).SetEase(_ease);
        }

        public void Disappear()
        {
            _rectTransform.DOAnchorPos(_hidedAnchorPosition, _duration).SetEase(_ease);
        }

        public async UniTask DissapearAsync(CancellationToken cts)
        {
            await _rectTransform.DOAnchorPos(_hidedAnchorPosition, _duration)
                .SetEase(_ease)
                .ToUniTask(cancellationToken: cts);
        }
    }
}