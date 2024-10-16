using Scripts.Interfaces.Data;
using Scripts.Interfaces.SDK;

namespace Scripts.SDK.Advertisment
{
    public class AdvertismentController
    {
        private readonly IAdvertismentData _addData;
        private readonly IAdvertismentService _advertismentService;

        public AdvertismentController(IAdvertismentData addData, IAdvertismentService advertismentService)
        {
            _addData = addData;
            _advertismentService = advertismentService;

            HandleAd();
        }

        public bool IsAddHided => _addData.IsAdHided;

        public void ShowIntersticialAd()
        {
            if (IsAddHided == false)
                _advertismentService.ShowInterstitialAd();
        }

        public void RemoveAd()
        {
            _addData.RemoveAd();
            HandleAd();
        }

        private void HandleAd()
        {
            if (IsAddHided == false)
                _advertismentService.ShowSticky();
            else
                _advertismentService.HideSticky();
        }
    }
}