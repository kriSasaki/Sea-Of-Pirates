using System.Collections.Generic;
using Scripts.Configs.ShopItems;
using YG.Utils.Pay;

namespace Scripts.Systems.Shop.Items
{
    public class BundleItem : InAppItem
    {
        private readonly List<InAppItem> _items;

        public BundleItem(List<InAppItem> items, BundleItemConfig config, Purchase itemData)
            : base(config, itemData)
        {
            _items = items;
        }

        public override string AmountText => string.Empty;

        public override void Get()
        {
            foreach (InAppItem item in _items)
            {
                if (item.IsAvaliable)
                    item.Get();
            }
        }
    }
}