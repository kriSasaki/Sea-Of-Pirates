using Project.Configs.ShopItems;
using Project.Interfaces.Data;
using Project.Systems.Stats;
using UnityEngine;

namespace Project.Systems.Shop.Items
{
    public abstract class GameItem : ShopItem
    {
        protected readonly IPlayerStorage PlayerStorage;

        public GameItem(GameItemConfig config, IPlayerStorage playerStorage)
            : base(config)
        {
            Price = config.Price;
            PlayerStorage = playerStorage;
        }

        public GameResourceAmount Price { get; }

        public bool CanBuy => PlayerStorage.CanSpend(Price);

        public override Sprite PriceSprite => Price.Resource.Sprite;

        public override string PriceAmountText => Price.Amount.ToString();
    }
}