using System;
using System.Collections.Generic;
using Scripts.Configs.ShopItems;
using Scripts.Interfaces.Data;
using Scripts.SDK.Advertisment;
using Scripts.Systems.Shop.Items;
using YG.Utils.Pay;

namespace Scripts.Systems.Shop
{
    public class ShopItemFactory
    {
        private readonly IPlayerStorage _playerStorage;
        private readonly AdvertismentController _advertismentController;

        public ShopItemFactory(IPlayerStorage playerStorage, AdvertismentController advertismentController)
        {
            _playerStorage = playerStorage;
            _advertismentController = advertismentController;
        }

        public GameItem Create(GameItemConfig config)
        {
            if (config is GameResourceItemConfig resourceConfig)
                return new GameResourceItem(resourceConfig, _playerStorage);

            throw new NotImplementedException();
        }

        public InAppItem Create(InAppItemConfig config, Purchase product)
        {
            if (config is InAppResourceItemConfig resourceConfig)
                return new InAppResourceItem(_playerStorage, resourceConfig, product);

            if (config is AddRemovalConfig addRemovalConfig)
                return new AddRemovalItem(_advertismentController, addRemovalConfig, product);

            if (config is BundleItemConfig bundleConfig)
                return new BundleItem(CreateBundle(bundleConfig, product), bundleConfig, product);

            throw new NotImplementedException();
        }

        private List<InAppItem> CreateBundle(BundleItemConfig bundleConfig, Purchase itemData)
        {
            List<InAppItem> items = new List<InAppItem>();

            foreach (InAppItemConfig config in bundleConfig.Items)
            {
                items.Add(Create(config, itemData));
            }

            return items;
        }
    }
}