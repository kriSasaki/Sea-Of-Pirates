using Project.Interfaces.Data;
using Project.Interfaces.SDK;

namespace Project.SDK.Advertisment
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
            _addData.IsAdHided = true;
            _addData.Save();

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