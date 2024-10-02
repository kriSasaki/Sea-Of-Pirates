using System;
using System.Collections.Generic;
using System.Linq;
using Project.Configs.ShopItems;
using Project.Interfaces.Data;
using Project.SDK.InApp;
using Project.Systems.Shop.Items;
using Project.UI.Shop;
using YG;
using YG.Utils.Pay;
using Zenject;

namespace Project.Systems.Shop
{
    public class ShopSystem : IInitializable, IDisposable
    {
        private readonly IPlayerStorage _playerStorage;
        private readonly IBillingService _billingService;
        private readonly ShopItemFactory _shopItemfactory;
        private readonly ShopItemsConfigs _shopItemsConfigs;
        private readonly ShopWindow _shopWindow;
        private readonly ShopButton _shopButton;

        private Dictionary<string, InAppItem> _inAppCatalogue = new();

        private bool _isItemsLoaded = false;

        public ShopSystem(
            IPlayerStorage playerStorage,
            IBillingService billingService,
            ShopItemFactory shopItemfactory,
            ShopItemsConfigs shopItemsConfigs,
            ShopWindow shopWindow,
            ShopButton shopButtom)
        {
            _playerStorage = playerStorage;
            _billingService = billingService;
            _shopItemfactory = shopItemfactory;
            _shopItemsConfigs = shopItemsConfigs;
            _shopWindow = shopWindow;
            _shopButton = shopButtom;

            YandexGame.PurchaseSuccessEvent += OnBuyInApp;
        }

        public void Initialize()
        {
            _shopButton.Bind(OpenShop);
            _billingService.LoadProductCatalog(LoadInAppItems);
        }

        private void OpenShop()
        {
            _shopWindow.Open();

            if (_isItemsLoaded)
                return;

            LoadShopItems();
        }

        private void LoadShopItems()
        {
            LoadGameItems();
            SetInAppItems();

            _isItemsLoaded = true;
        }

        private void LoadGameItems()
        {
            foreach (GameItemConfig config in _shopItemsConfigs.GameItemsConfigs)
            {
                GameItem item = _shopItemfactory.Create(config);
                _shopWindow.CreateItemSlot(item, () => BuyItem(item));
            }
        }

        private void LoadInAppItems(Purchase[] products)
        {
            foreach (var config in _shopItemsConfigs.InAppItemsConfigs)
            {
                Purchase itemData = products.FirstOrDefault(p => p.id == config.ID);

                if (itemData == null)
                    continue;

                InAppItem item = _shopItemfactory.Create(config, itemData);
                _inAppCatalogue.Add(item.ID, item);
            }

            YandexGame.ConsumePurchases();
        }

        private void SetInAppItems()
        {
            foreach (InAppItem item in _inAppCatalogue.Values)
            {
                if (item.IsAvaliable == false)
                    continue;

                _shopWindow.CreateItemSlot(item, () => BuyItem(item));
            }
        }

        private void BuyItem(InAppItem item)
        {
            YandexGame.BuyPayments(item.ID);
        }

        private void BuyItem(GameItem item)
        {
            if (_playerStorage.TrySpendResource(item.Price))
                GetShopItem(item);
        }

        private void GetShopItem(ShopItem item)
        {
            item.Get();
        }

        private void OnBuyInApp(string id)
        {
            InAppItem item = _inAppCatalogue[id];

            GetShopItem(item);
            _shopWindow.CheckSlots();
        }

        public void Dispose()
        {
            YandexGame.PurchaseSuccessEvent -= OnBuyInApp;
        }
    }
}