using System;
using YG.Utils.Pay;

namespace Scripts.SDK.InApp
{
    public interface IBillingService
    {
        void LoadProductCatalog(Action<Purchase[]> onLoadCallback);
    }
}