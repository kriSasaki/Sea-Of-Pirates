using Cysharp.Threading.Tasks;
using DG.Tweening;
using System.Threading;
using UnityEngine;

namespace Project.UI
{
    public class UiCanvas : MonoBehaviour
    {
        private const float MinAlpha = 0f;
        private const float MaxAlpha = 1f;

        [SerializeField] private CanvasGroup _canvasGroup;

        public void Enable()
        {
            _canvasGroup.alpha = MaxAlpha;
            _canvasGroup.interactable = true;
        }

        public void Disable()
        {
            _canvasGroup.alpha = MinAlpha;
            _canvasGroup.interactable = false;
        }

        public async UniTask EnableAsync(float duration, CancellationToken token)
        {
            await _canvasGroup.DOFade(MaxAlpha, duration).WithCancellation(token);
            Enable();
        }
    }
}