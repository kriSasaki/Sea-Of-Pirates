using Scripts.Configs.ShopItems;
using Scripts.Interfaces.Data;
using Scripts.Systems.Data;
using Scripts.Utils.Extensions;
using YG.Utils.Pay;

namespace Scripts.Systems.Shop.Items
{
    public class InAppResourceItem : InAppItem
    {
        private readonly IPlayerStorage _playerStorage;
        private readonly InAppResourceItemConfig _config;

        public InAppResourceItem(IPlayerStorage playerStorage, InAppResourceItemConfig config, Purchase itemData)
            : base(config, itemData)
        {
            _playerStorage = playerStorage;
            _config = config;
        }

        public GameResourceAmount Item => _config.Item;
        public override string AmountText => Item.Amount.ToNumericalString();

        public override void Get()
        {
            _playerStorage.AddResource(Item);
        }
    }
}