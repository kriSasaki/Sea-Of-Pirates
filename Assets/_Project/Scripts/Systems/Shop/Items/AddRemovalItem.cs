using Project.Configs.ShopItems;
using Project.SDK.Advertisment;
using YG;
using YG.Utils.Pay;

namespace Project.Systems.Shop.Items
{
    public class AddRemovalItem : InAppItem
    {
        private readonly AdvertismentController _advertismentController;

        public AddRemovalItem(
            AdvertismentController advertismentController,
            AddRemovalConfig config,
            Purchase itemData)
            : base(config, itemData)
        {
            _advertismentController = advertismentController;
        }

        public override bool IsAvaliable => _advertismentController.IsAddHided == false;

        public override string AmountText => string.Empty;

        public override void Get()
        {
            _advertismentController.RemoveAd();
        }
    }
}