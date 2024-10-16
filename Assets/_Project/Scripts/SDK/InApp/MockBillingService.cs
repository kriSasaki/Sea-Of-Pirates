using System;
using System.Collections.Generic;
using Scripts.Configs.ShopItems;
using YG.Utils.Pay;

namespace Scripts.SDK.InApp
{
    public class MockBillingService : IBillingService
    {
        private readonly ShopItemsConfigs _shopItemsConfigs;

        public MockBillingService(ShopItemsConfigs shopItemsConfigs)
        {
            _shopItemsConfigs = shopItemsConfigs;
        }

        public void LoadProductCatalog(Action<Purchase[]> onLoadCallback)
        {
            List<Purchase> products = new List<Purchase>();

            foreach (var config in _shopItemsConfigs.InAppItemsConfigs)
            {
                products.Add(new Purchase() { id = config.ID, price = "4 YAN" });
            }

            onLoadCallback(products.ToArray());
        }
    }
}