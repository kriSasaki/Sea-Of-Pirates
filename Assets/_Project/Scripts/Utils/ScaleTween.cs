using DG.Tweening;
using UnityEngine;

namespace Project.Utils
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

        public void Run()
        {
            _transform.DOScale(_scale, _duration)
                .From()
                .SetEase(_ease)
                .OnComplete(() => _transform.localScale = _originScale);
        }
    }
}