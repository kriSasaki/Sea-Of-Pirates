using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Scripts.Utils.Extensions;
using UnityEngine;

namespace Scripts.Utils.Tweens
{
    [System.Serializable]
    public class SinkTween
    {
        [SerializeField] private float _sinkDeep = 5f;
        [SerializeField] private float _sinkDuration = 3f;
        [SerializeField] private float _sinkAngle = 90f;
        [SerializeField] private Ease _sinkEase;

        private Transform _transform;

        public void Initialize(Transform transform)
        {
            _transform = transform;
        }

        public async UniTask Sink(CancellationToken token)
        {
            await UniTask.WhenAll(
                    _transform.DOMoveY(-_sinkDeep, _sinkDuration)
                        .SetEase(_sinkEase)
                        .SetRelative()
                        .WithCancellation(token),

                    _transform.DORotate(_transform.rotation.eulerAngles.WithX(_sinkAngle), _sinkDuration)
                        .WithCancellation(token));
        }
    }
}