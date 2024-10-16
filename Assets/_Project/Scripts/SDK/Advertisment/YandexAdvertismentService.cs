using Scripts.Interfaces.SDK;
using YG;

namespace Scripts.SDK.Advertisment
{
    public class YandexAdvertismentService : AdvertismentService, IAdvertismentService
    {
        public override void ShowInterstitialAd()
        {
            YandexGame.FullscreenShow();
        }

        public override void ShowRewardAd(int rewardAmount)
        {
            YandexGame.RewVideoShow(rewardAmount);
        }

        public override void HideSticky()
            => YandexGame.StickyAdActivity(false);

        public override void ShowSticky()
            => YandexGame.StickyAdActivity(true);
    }
}