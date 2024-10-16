using Scripts.Configs.ShopItems;
using Scripts.Interfaces.Data;
using Scripts.Systems.Data;
using Scripts.Utils.Extensions;
using UnityEngine;

namespace Scripts.Systems.Shop.Items
{
    public class GameResourceItem : GameItem
    {
        private readonly IPlayerStorage _playerStorage;
        public GameResourceItem(GameResourceItemConfig config, IPlayerStorage playerStorage)
            : base(config, playerStorage)
        {
            Item = config.Item;
            _playerStorage = playerStorage;
        }

        public GameResourceAmount Item { get; }
        public override Sprite PriceSprite => Price.Resource.Sprite;
        public override string AmountText => Item.Amount.ToNumericalString();

        public override void Get()
        {
            _playerStorage.AddResource(Item);
        }
    }
}