using DG.Tweening;
using UnityEngine;

namespace Scripts.Utils.Tweens
{
    [System.Serializable]
    public class ScaleTween
    {
        [SerializeField] private float _scale = 1.3f;
        [SerializeField] private float _duration = 0.3f;
        [SerializeField] private Ease _ease = Ease.Linear;

        private Transform _transform;
        private Vector3 _originScale;

        public void Initialize(Transform transform)
        {
            _transform = transform;
            _originScale = _transform.localScale;
        }

        public void RunFrom()
        {
            _transform.DOScale(_scale, _duration)
                .From()
                .SetEase(_ease)
                .OnComplete(() => _transform.localScale = _originScale);
        }

        public void RunTo()
        {
            _transform.DOScale(_scale, _duration)
                .SetEase(_ease);
        }
    }
}