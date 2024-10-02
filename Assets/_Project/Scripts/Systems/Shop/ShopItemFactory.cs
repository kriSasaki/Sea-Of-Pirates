using System;
using System.Collections.Generic;
using Project.Configs.ShopItems;
using Project.Interfaces.Data;
using Project.SDK.Advertisment;
using Project.Systems.Shop.Items;
using YG;
using YG.Utils.Pay;

namespace Project.Systems.Shop
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
            List<InAppItem> _items = new List<InAppItem>();

            foreach (InAppItemConfig config in bundleConfig.Items)
            {
                _items.Add(Create(config, itemData));
            }

            return _items;
        }
    }
}