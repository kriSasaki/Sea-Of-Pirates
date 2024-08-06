using System;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmWindow : UiWindow
{
    [SerializeField] private Button _confirmButton;
    [SerializeField] private ShopItemView _shopItemView;

    public void Open(ShopItem shopItem, Action confirmCallback)
    {
        base.Show();

        _shopItemView.Set(shopItem);
        _confirmButton.onClick.AddListener(() => ConfirmPurchase(confirmCallback));
    }

    public void Open(InAppItemData itemData, Action confirmCallback)
    {
        base.Show();

        _shopItemView.Set(itemData);
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