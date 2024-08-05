using Project.Systems.Stats;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmWindow : UiWindow
{
    [SerializeField] private Button _confirmButton;
    [SerializeField] private Image _goodImage;
    [SerializeField] private TMP_Text _goodAmount;
    [SerializeField] private Image _priceImage;
    [SerializeField] private TMP_Text _priceAmount;

    public void Show(ShopItem shopItem, Action confirmCallback)
    {
        base.Show();

        GameResourceAmount good = shopItem.Good;
        GameResourceAmount price = shopItem.Price;

        _goodImage.sprite = good.Resource.Sprite;
        _goodAmount.text = good.Amount.ToString();
        _priceImage.sprite = price.Resource.Sprite;
        _priceAmount.text = price.Amount.ToString();

        _confirmButton.onClick.AddListener(() => ConfirmPurchase(confirmCallback)); 
    }

    private void ConfirmPurchase(Action confirmCallback)
    {
        confirmCallback.Invoke();
        Hide();
    }

    public override void Hide()
    {
        base.Hide();

        _confirmButton.onClick.RemoveAllListeners();
    }
}