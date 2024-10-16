using Scripts.Interfaces.SDK;

namespace Scripts.SDK.Advertisment
{
    public abstract class AdvertismentService : IAdvertismentService
    {
        public bool IsAdsPlaying { get; private set; } = false;

        public abstract void ShowInterstitialAd();

        public abstract void ShowRewardAd(int rewardAmount);

        public abstract void ShowSticky();

        public abstract void HideSticky();
    }
}