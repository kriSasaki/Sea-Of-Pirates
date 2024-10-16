using YG;

namespace Scripts.Utils
{
    public static class MobileDetector
    {
        public static bool IsMobile()
        {
            return YandexGame.EnvironmentData.isMobile || YandexGame.EnvironmentData.isTablet || IsIPadPro();
        }

        private static bool IsIPadPro()
        {
            return WebGLBrowserCheck.IsMobileBrowser();
        }
    }
}