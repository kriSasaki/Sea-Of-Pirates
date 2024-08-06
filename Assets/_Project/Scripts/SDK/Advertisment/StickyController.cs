using Project.Interfaces.Data;

namespace Project.SDK.Advertisment
{
    public class StickyController
    {
        private readonly IStickyData _stickyData;

        public StickyController(IStickyData stickyData)
        {
            _stickyData = stickyData;
            HandleSticky();
        }

        public bool IsStickyActive => _stickyData.IsStickyActive;

        public void HideSticky()
        {
            _stickyData.IsStickyActive = false;
            _stickyData.Save();

            HandleSticky();
        }

        private void HandleSticky()
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