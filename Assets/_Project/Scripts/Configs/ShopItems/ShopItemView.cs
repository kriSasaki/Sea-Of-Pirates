using Project.Systems.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemView : MonoBehaviour
{
    [SerializeField] private Image _itemImage;
    [SerializeField] private TMP_Text _itemAmount;
    [SerializeField] private Image _priceImage;
    [SerializeField] private TMP_Text _priceAmount;

    public void Set(ShopItem shopItem)
    {
        GameResourceAmount item = shopItem.Item;
        GameResourceAmount price = shopItem.Price;

        _itemImage.sprite = item.Resource.Sprite;
        _itemAmount.text = item.Amount.ToString();
        _priceImage.sprite = price.Resource.Sprite;
        _priceAmount.text = price.Amount.ToString();

        _priceImage.enabled = true;
    }

    public void Set(InAppItemData itemData)
    {
        _itemImage.sprite = itemData.ItemSprite;
        _itemAmount.text = itemData.ItemAmount;
        _priceImage.enabled = false;
        _priceAmount.text = itemData.PriceCurrencyCode + " " + itemData.PriceValue;
    }
}