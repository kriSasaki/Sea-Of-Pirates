using DG.Tweening;
using UnityEngine;

namespace Scripts.Utils.Tweens
{
    [System.Serializable]
    public class PunchShipTween
    {
        [SerializeField] private float _force = 6f;
        [SerializeField] private float _duration = 0.8f;
        [SerializeField] private int _vibrato = 3;
        [SerializeField, Range(0f, 1f)] private float _elacticity = 1f;
        [SerializeField] private Ease _ease = Ease.InOutSine;

        private Transform _transform;
        private bool _isPunching = false;

        public void Initialize(Transform transform)
        {
            _transform = transform;
        }

        public void Punch()
        {
            if (_isPunching)
                return;

            _isPunching = true;

            _transform.DOPunchRotation(Vector3.forward * _force, _duration, _vibrato, _elacticity)
                .SetEase(_ease)
                .OnComplete(() => _isPunching = false);
        }
    }
}