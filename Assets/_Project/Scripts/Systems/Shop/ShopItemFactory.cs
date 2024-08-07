﻿using Agava.YandexGames;
using Project.Configs.ShopItems;
using Project.Interfaces.Data;
using Project.SDK.Advertisment;
using Project.Systems.Shop.Items;
using System;
using System.Collections.Generic;

namespace Project.Systems.Shop
{
    public class ShopItemFactory
    {
        private IPlayerStorage _playerStorage;
        private AdvertismentController _advertismentController;

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

        public InAppItem Create(InAppItemConfig config, CatalogProduct itemData)
        {
            if (config is InAppResourceItemConfig resourceConfig)
                return new InAppResourceItem(_playerStorage, resourceConfig, itemData);

            if (config is AddRemovalConfig addRemovalConfig)
                return new AddRemovalItem(_advertismentController, addRemovalConfig, itemData);

            if (config is BundleItemConfig bundleConfig)
                return new BundleItem(CreateBundle(bundleConfig, itemData), bundleConfig, itemData);

            throw new NotImplementedException();
        }

        private List<InAppItem> CreateBundle(BundleItemConfig bundleConfig, CatalogProduct itemData)
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