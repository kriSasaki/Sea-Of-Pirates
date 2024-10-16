namespace Scripts.Interfaces.SDK
{
    public interface IAdvertismentService
    {
        bool IsAdsPlaying { get; }

        void ShowInterstitialAd();

        void ShowRewardAd(int rewardAmount);

        void ShowSticky();

        void HideSticky();
    }
}