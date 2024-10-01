using Project.Interfaces.SDK;
using Project.Systems.Pause;
using System;
using YG;

namespace Project.SDK.Advertisment
{
    public class YandexAdvertismentService : AdvertismentService, IAdvertismentService
    {
        //public YandexAdvertismentService(PauseService pauseService)
        //    : base(pauseService)
        //{
        //}

        public override void ShowInterstitialAd()
        {
            //Agava.YandexGames.InterstitialAd.Show(OpenAd, OnCloseInterstitial);
            YandexGame.FullscreenShow();

            //void OnCloseInterstitial(bool wasShown)
            //{
            //    CloseAd();
            //}
        }

        public override void ShowRewardAd(int rewardAmount)
        {
            //Agava.YandexGames.VideoAd.Show(OpenAd, () => onRewardedCallback(), CloseAd);

            YandexGame.RewVideoShow(rewardAmount);


        }

        public override void HideSticky()
            => YandexGame.StickyAdActivity(false);

        public override void ShowSticky()
            => YandexGame.StickyAdActivity(true);
    }
}
