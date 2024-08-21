using Project.Interfaces.Hold;
using TMPro;
using UnityEngine;
using Zenject;

namespace Project.UI.Bars
{
    public class PlayerHoldBar : FadeableBar
    {
        [SerializeField] private TMP_Text _amountLabel;

        private IPlayerHold _hold;

        private void OnDestroy()
        {
            _hold.CargoChanged -= OnCargoChanged;
        }

        [Inject]
        private void Construct(IPlayerHold hold)
        {
            _hold = hold;
            _hold.CargoChanged += OnCargoChanged;
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