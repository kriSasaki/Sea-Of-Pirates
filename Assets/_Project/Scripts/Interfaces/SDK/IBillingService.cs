using System;
using YG.Utils.Pay;

namespace Project.SDK.InApp
{
    public interface IBillingService
    {
        void LoadProductCatalog(Action<Purchase[]> onLoadCallback);
    }
}