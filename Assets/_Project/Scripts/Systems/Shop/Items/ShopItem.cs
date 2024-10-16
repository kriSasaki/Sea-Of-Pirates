using Scripts.Configs.ShopItems;
using UnityEngine;

namespace Scripts.Systems.Shop.Items
{
    public abstract class ShopItem
    {
        public ShopItem(ShopItemConfig config)
        {
            Sprite = config.Sprite;
        }

        public virtual bool IsAvaliable { get; } = true;
        public Sprite Sprite { get; }
        public abstract string AmountText { get; }
        public abstract Sprite PriceSprite { get; }
        public abstract string PriceAmountText { get; }

        public abstract void Get();
    }
}