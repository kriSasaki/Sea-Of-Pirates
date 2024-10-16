using Scripts.Configs.UI;
using Scripts.Systems.Shop.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Scripts.UI.Shop
{
    public class ShopItemView : MonoBehaviour
    {
        [SerializeField] private Image _itemImage;
        [SerializeField] private TMP_Text _itemAmount;
        [SerializeField] private Image _priceImage;
        [SerializeField] private TMP_Text _priceAmount;

        [SerializeField] private Image _background;
        [SerializeField] private Image _cover;

        public void Set(ShopItem item, ItemViewColorConfig config = null)
        {
            if (config != null)
            {
                _background.color = config.BackgroundColor;
                _cover.color = config.CoverColor;
            }

            _itemImage.sprite = item.Sprite;
            _itemAmount.text = item.AmountText;

            _priceAmount.text = item.PriceAmountText;

            _priceImage.sprite = item.PriceSprite;
            _priceImage.gameObject.SetActive(item.PriceSprite != null);
        }
    }
}