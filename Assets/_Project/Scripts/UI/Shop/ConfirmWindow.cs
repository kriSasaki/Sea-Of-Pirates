using System;
using Scripts.Systems.Shop.Items;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Shop
{
    public class ConfirmWindow : UiWindow
    {
        [SerializeField] private Button _confirmButton;
        [SerializeField] private ShopItemView _shopItemView;

        public void Open(ShopItem item, Action confirmCallback)
        {
            Show();

            _shopItemView.Set(item);
            _confirmButton.onClick.AddListener(() => ConfirmPurchase(confirmCallback));
        }

        public override void Hide()
        {
            base.Hide();

            _confirmButton.onClick.RemoveAllListeners();
        }

        private void ConfirmPurchase(Action confirmCallback)
        {
            confirmCallback.Invoke();
            Hide();
        }
    }
}