using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Bars
{
    public class FillableBar : MonoBehaviour
    {
        [SerializeField] private Image _filler;
        [SerializeField] private float _fillDuration = 0.1f;
        [SerializeField] private Ease _fillEase = Ease.Linear;

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

        protected virtual void OnFill()
        {
        }
    }
}