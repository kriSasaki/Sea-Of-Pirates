using System;
using System.Collections.Generic;
using System.Linq;
using Scripts.Configs.ShopItems;
using Scripts.Interfaces.Data;
using Scripts.SDK.InApp;
using Scripts.Systems.Shop.Items;
using Scripts.UI.Shop;
using YG;
using YG.Utils.Pay;
using Zenject;

namespace Scripts.Systems.Shop
{
    public class ShopSystem : IInitializable, IDisposable
    {
        private readonly IPlayerStorage _playerStorage;
        private readonly IBillingService _billingService;
        private readonly ShopItemFactory _shopItemFactory;
        private readonly ShopItemsConfigs _shopItemsConfigs;
        private readonly ShopWindow _shopWindow;
        private readonly ShopButton _shopButton;

        private Dictionary<string, InAppItem> _inAppCatalogue = new();

        private bool _isItemsLoaded = false;

        public ShopSystem(
            IPlayerStorage playerStorage,
            IBillingService billingService,
            ShopItemFactory shopItemFactory,
            ShopItemsConfigs shopItemsConfigs,
            ShopWindow shopWindow,
            ShopButton shopButton)
        {
            _playerStorage = playerStorage;
            _billingService = billingService;
            _shopItemFactory = shopItemFactory;
            _shopItemsConfigs = shopItemsConfigs;
            _shopWindow = shopWindow;
            _shopButton = shopButton;

            YandexGame.PurchaseSuccessEvent += OnBuyInApp;
        }

        public void Initialize()
        {
            _shopButton.Bind(OpenShop);
            _billingService.LoadProductCatalog(LoadInAppItems);
        }

        public void Dispose()
        {
            YandexGame.PurchaseSuccessEvent -= OnBuyInApp;
        }

        private void OpenShop()
        {
            _shopWindow.Open();

            if (_isItemsLoaded)
                return;

            SetShopItems();
        }

        private void SetShopItems()
        {
            SetGameItems();

            if (YandexGame.auth == true)
            {
                SetInAppItems();
            }
            else
            {
                _shopWindow.HideInAppHeader();
            }

            _isItemsLoaded = true;
        }

        private void SetGameItems()
        {
            foreach (GameItemConfig config in _shopItemsConfigs.GameItemsConfigs)
            {
                GameItem item = _shopItemFactory.Create(config);
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

                InAppItem item = _shopItemFactory.Create(config, itemData);
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

    }
}