using Scripts.Interfaces.Hold;
using TMPro;
using UnityEngine;
using Zenject;

namespace Scripts.UI.Bars
{
    public class PlayerHoldBar : FadeableBar
    {
        [SerializeField] private TMP_Text _amountLabel;
        [SerializeField] private Color _warningColor = Color.red;
        [SerializeField] private int _warningLoops = 6;
        [SerializeField] private float _loopDuration = 0.2f;

        private IPlayerHold _hold;

        private void OnDestroy()
        {
            _hold.CargoChanged -= OnCargoChanged;
            _hold.Filled -= OnCargoFilled;
        }

        [Inject]
        private void Construct(IPlayerHold hold)
        {
            _hold = hold;
            _hold.CargoChanged += OnCargoChanged;
            _hold.Filled += OnCargoFilled;
        }

        private void OnCargoFilled()
        {
           WarningLerpColor(_warningColor, _warningLoops, _loopDuration);
        }

        private void OnCargoChanged(int currentCargo)
        {
            _amountLabel.text = $"{currentCargo} / {_hold.CargoSize}";

            Fill(currentCargo, _hold.CargoSize);

            if (_hold.IsEmpty)
                TryFade(() => _hold.IsEmpty);
        }
    }
}