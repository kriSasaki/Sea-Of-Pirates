using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Bars
{
    public class FillableBar : MonoBehaviour
    {
        [SerializeField] private Image _filler;
        [SerializeField] private float _fillDuration = 0.1f;
        [SerializeField] private Ease _fillEase = Ease.Linear;

        private Color _originColor;

        private void Awake()
        {
            _originColor = _filler.color;
        }

        public void Set(float currentValue, float maxValue)
        {
            _filler.fillAmount = currentValue / maxValue;
        }

        public void Fill(float currentValue, float maxValue)
        {
            OnFill();

            _filler
                .DOFillAmount(currentValue / maxValue, _fillDuration)
                .SetEase(_fillEase);
        }

        public async UniTask FillAsync(float currentValue, float maxValue)
        {
            OnFill();

            await _filler
                .DOFillAmount(currentValue / maxValue, _fillDuration)
                .SetEase(_fillEase)
                .WithCancellation(destroyCancellationToken);
        }

        public void LerpColor(Color fromColor, float duration)
        {
            _filler.color = fromColor;
            _filler.DOColor(_originColor, duration);
        }

        protected void WarningLerpColor(Color fromColor, int loops, float loopDuration)
        {
            if (_filler.color != _originColor)
                return;

            _filler.color = fromColor;

            _filler.DOColor(_originColor, loopDuration)
                .SetLoops(loops)
                .OnComplete(() => _filler.color = _originColor);
        }

        protected virtual void OnFill()
        {
        }
    }
}