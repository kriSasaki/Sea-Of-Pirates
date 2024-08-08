using Agava.YandexGames;
using Project.Configs.ShopItems;
using UnityEngine;

namespace Project.Systems.Shop.Items
{
    public abstract class InAppItem : ShopItem
    {
        private readonly CatalogProduct _product;

        protected InAppItem(InAppItemConfig config, CatalogProduct product)
            : base(config)
        {
            _product = product;
        }

        public string ID => _product.id;
        public override string PriceAmountText => _product.price;
        public override Sprite PriceSprite => null;
    }
}