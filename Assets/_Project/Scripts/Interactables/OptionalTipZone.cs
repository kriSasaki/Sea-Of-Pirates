using Lean.Localization;
using UnityEngine;
using YG;

namespace Project.Interactables
{
    public class OptionalTipZone : TipZone
    {
        [SerializeField, LeanTranslationName] private string _mobileTipToken;

        protected override void ShowTip()
        {
            if (IsMobile())
            {
                Typewriter.ShowText(GetTipMessage(_mobileTipToken));
            }
            else
            {
                Typewriter.ShowText(GetTipMessage(TipToken));
            }
        }

        private bool IsMobile()
        {
            return YandexGame.EnvironmentData.isMobile || YandexGame.EnvironmentData.isTablet || WebGLBrowserCheck.IsMobileBrowser();
        }
    }
}