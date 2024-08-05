using Project.Systems.Stats;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemSlot : MonoBehaviour
{
    [SerializeField] private Button _selectButton;
    [SerializeField] private Image _goodImage;
    [SerializeField] private TMP_Text _goodAmount;
    [SerializeField] private Image _priceImage;
    [SerializeField] private TMP_Text _priceAmount;

    private ShopItem _shopItem;

    public event Action<ShopItem> Selected;

    private void Awake()
    {
        _selectButton.onClick.AddListener(OnItemSelected);
    }

    private void OnDestroy()
    {
        _selectButton.onClick.RemoveListener(OnItemSelected);
    }

    private void OnItemSelected()
    {
        Selected?.Invoke(_shopItem);
    }

    public void Initialize(ShopItem shopItem)
    {
        _shopItem = shopItem;

        GameResourceAmount good = _shopItem.Good;
        GameResourceAmount price = _shopItem.Price;

        _goodImage.sprite = good.Resource.Sprite;
        _goodAmount.text = good.Amount.ToString();
        _priceImage.sprite = price.Resource.Sprite;
        _priceAmount.text = price.Amount.ToString();
    }
}