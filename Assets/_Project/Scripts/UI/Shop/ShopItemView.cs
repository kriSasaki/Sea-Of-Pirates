using Project.Systems.Shop.Items;
using Project.Systems.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Project.UI.Shop
{
    public class ShopItemView : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text _itemAmount;
        [SerializeField] private Image _priceImage;
        [SerializeField] private TMP_Text _priceAmount;

        public void Set(ShopItem item)
        {
            _itemImage.sprite = item.Sprite;
            _itemAmount.text = item.AmountText;

            _priceAmount.text = item.PriceAmountText;

            _priceImage.sprite = item.PriceSprite;
            _priceImage.gameObject.SetActive(item.PriceSprite != null);
        }
    }
}