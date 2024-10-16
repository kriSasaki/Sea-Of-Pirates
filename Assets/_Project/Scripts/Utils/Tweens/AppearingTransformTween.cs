using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Scripts.Utils.Tweens
{
    [System.Serializable]
    public class AppearingTransformTween
    {
        private const float _dissapearedScale = 0f;

        [SerializeField] private float _duration = 0.3f;
        [SerializeField] private Ease _ease = Ease.Linear;

        private Transform _transform;
        private Vector3 _originScale;
        private Tweener _tween;

        public void Initialize(Transform transform)
        {
            _transform = transform;
            _originScale = _transform.localScale;
        }

        public void Appear()
        {
            _tween?.Kill();
            _tween = _transform.DOScale(_originScale, _duration).SetEase(_ease);
        }

        public async UniTask AppearAsync()
        {
            await _transform
                .DOScale(_originScale, _duration)
                .SetEase(_ease);
        }

        public void Disappear(Action onDissapearCallback = null)
        {
            _tween?.Kill();
            _tween = _transform.DOScale(_dissapearedScale, _duration)
                .SetEase(_ease)
                .OnComplete(() => onDissapearCallback?.Invoke());
        }

        public async UniTask DisappearAsync()
        {
            await _transform
                .DOScale(_dissapearedScale, _duration)
                .SetEase(_ease);
        }
    }
}