using System;
using YG;
using YG.Utils.Pay;

namespace Scripts.SDK.InApp
{
    public class YandexBillingService : IBillingService
    {
        public void LoadProductCatalog(Action<Purchase[]> onLoadCallback)
        {
            onLoadCallback(YandexGame.purchases);
        }
    }
}