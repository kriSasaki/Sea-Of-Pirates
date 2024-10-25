using System;
using YG.Utils.Pay;

namespace Scripts.SDK
{
    public interface IBillingService
    {
        void LoadProductCatalog(Action<Purchase[]> onLoadCallback);
    }
}