using System;
using YG.Utils.Pay;

namespace Scripts.Interfaces.SDK
{
    public interface IBillingService
    {
        void LoadProductCatalog(Action<Purchase[]> onLoadCallback);
    }
}