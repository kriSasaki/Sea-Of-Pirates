using Agava.YandexGames;
using Project.Configs.ShopItems;
using UnityEngine;

namespace Project.Systems.Shop.Items
{
    public abstract class InAppItem : ShopItem
    {
        private readonly CatalogProduct _itemData;

        protected InAppItem(InAppItemConfig config, CatalogProduct itemData)
            : base(config)
        {
            _itemData = itemData;
        }

        public string ID => _itemData.id;
        public override string PriceAmountText => $"{_itemData.priceValue} {_itemData.priceCurrencyCode}";
        public override Sprite PriceSprite => null;
    }
}