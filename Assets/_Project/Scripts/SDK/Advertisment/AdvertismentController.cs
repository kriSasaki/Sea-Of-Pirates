using Project.Interfaces.Data;

namespace Project.SDK.Advertisment
{
    public class AdvertismentController
    {
        private readonly IAdvertismentData _addData;

        public AdvertismentController(IAdvertismentData addData)
        {
            _addData = addData;
            HandleAdd();
        }

        public bool IsAddActive => _addData.IsAddActive;

        public void HideAdd()
        {
            _addData.IsAddActive = false;
            _addData.Save();

            HandleAdd();
        }

        private void HandleAdd()
        {
#if !UNITY_EDITOR && UNITY_WEBGL
        if (IsStickyActive)
            Agava.YandexGames.StickyAd.Show();
        else
            Agava.YandexGames.StickyAd.Hide();
#endif
        }
    }
}