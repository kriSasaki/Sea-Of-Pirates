using Project.Configs.ShopItems;
using Project.Interfaces.Data;
using Project.Systems.Stats;
using UnityEngine;

namespace Project.Systems.Shop.Items
{
    public class GameResourceItem : GameItem
    {
        public GameResourceItem(GameResourceItemConfig config, IPlayerStorage playerStorage)
            : base(config, playerStorage)
        {
            Item = config.Item;

        }
        public GameResourceAmount Item { get; }

        public override Sprite PriceSprite => Price.Resource.Sprite;
        public override string PriceAmountText => Price.Amount.ToString();
        public override string AmountText => Item.Amount.ToString();

        public override void Get()
        {
            PlayerStorage.AddResource(Item);
        }
    }
}