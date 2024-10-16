using Scripts.Configs.ShopItems;
using UnityEngine;
using YG.Utils.Pay;

namespace Scripts.Systems.Shop.Items
{
    public abstract class InAppItem : ShopItem
    {
        private readonly Purchase _product;

        protected InAppItem(InAppItemConfig config, Purchase product)
            : base(config)
        {
            _product = product;
        }

        public string ID => _product.id;
        public override string PriceAmountText => _product.price;
        public override Sprite PriceSprite => null;
    }
}