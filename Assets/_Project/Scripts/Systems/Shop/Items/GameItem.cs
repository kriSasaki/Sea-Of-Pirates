using Scripts.Configs.ShopItems;
using Scripts.Interfaces.Data;
using Scripts.Systems.Data;
using Scripts.Utils.Extensions;
using UnityEngine;

namespace Scripts.Systems.Shop.Items
{
    public abstract class GameItem : ShopItem
    {
        private readonly IPlayerStorage _playerStorage;

        public GameItem(GameItemConfig config, IPlayerStorage playerStorage)
            : base(config)
        {
            Price = config.Price;
            _playerStorage = playerStorage;
        }

        public GameResourceAmount Price { get; }

        public bool CanBuy => _playerStorage.CanSpend(Price);

        public override Sprite PriceSprite => Price.Resource.Sprite;

        public override string PriceAmountText => Price.Amount.ToNumericalString();
    }
}