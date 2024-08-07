using Project.Interfaces.Data;
using Project.Systems.Pause;
using System;

namespace Project.SDK.Advertisment
{
    public class AdvertismentService
    {
        private readonly PauseService _pauseService;
        private readonly IAdvertismentStatus _advertismentStatus;

        public AdvertismentService(PauseService pauseService, IAdvertismentStatus advertismentStatus)
        {
            _pauseService = pauseService;
            _advertismentStatus = advertismentStatus;
        }

        public bool IsAdsPlaying { get; private set; } = false;

        public void ShowInterstitialAd(Action onClose = null)
        {
            if (_advertismentStatus.IsAddActive == false)
                return;

#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.InterstitialAd.Show(OnOpenCallback, OnCloseInterstitial);
#else
            OnOpenCallback();
            OnCloseInterstitial(true);
#endif
            void OnCloseInterstitial(bool wasShown)
            {
                OnCloseCallback();
                onClose?.Invoke();
            }
        }

        public void ShowRewardAd(Action onSuccess)
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRewardedCallback, OnCloseCallback, OnErrorCallback);
#else
            OnOpenCallback();
            OnRewardedCallback();
            OnCloseCallback();

#endif
            void OnRewardedCallback()
            {
                onSuccess();
            }
        }

        private void OnCloseCallback()
        {
            IsAdsPlaying = false;
            _pauseService.Unpause();
        }

        private void OnOpenCallback()
        {
            IsAdsPlaying = true;
            _pauseService.Pause();
        }
    }
}