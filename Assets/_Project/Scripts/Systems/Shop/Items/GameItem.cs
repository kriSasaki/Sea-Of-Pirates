using Project.Configs.ShopItems;
using Project.Systems.Stats;
using UnityEngine;

namespace Project.Systems.Shop.Items
{
    public abstract class GameItem : ShopItem
    {
        public GameItem(GameItemConfig config)
            : base(config)
        {
            Price = config.Price;
        }

        public GameResourceAmount Price { get; }

        public override Sprite PriceSprite => Price.Resource.Sprite;

        public override string PriceAmountText => Price.Amount.ToString();
    }
}