using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace Scripts.UI.Bars
{
    [RequireComponent(typeof(CanvasGroup))]
    public class FadeableBar : FillableBar
    {
        private const float MinAlpha = 0f;
        private const float MaxAlpha = 1f;

        [SerializeField] private float _fadeDuration = 0.2f;
        [SerializeField] private Ease _fadeEase = Ease.Linear;
        [SerializeField] private float _fadeDelay = 1f;

        private CanvasGroup _canvasGroup;
        private Coroutine _fadeRoutine;
        private WaitForSeconds _waitDelay;

        public void TryFade(Func<bool> fadeCondition)
        {
            ClearFadeRoutine();
            _fadeRoutine = StartCoroutine(Fading(fadeCondition));
        }

        protected override void OnFill()
        {
            base.OnFill();

            ClearFadeRoutine();
            Unfade();
        }

        [Inject]
        private void Construct()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _waitDelay = new WaitForSeconds(_fadeDelay);
            Unfade();
        }

        private void ClearFadeRoutine()
        {
            if (_fadeRoutine != null)
                StopCoroutine(_fadeRoutine);

            _fadeRoutine = null;
        }

        private IEnumerator Fading(Func<bool> fadeCondition)
        {
            yield return _waitDelay;

            if (fadeCondition() == false)
                yield break;

            yield return _canvasGroup.DOFade(MinAlpha, _fadeDuration).SetEase(_fadeEase);
        }

        private void Unfade()
            => _canvasGroup.DOFade(MaxAlpha, _fadeDuration).SetEase(_fadeEase);
    }
}