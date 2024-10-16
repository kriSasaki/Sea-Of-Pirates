using Lean.Localization;
using Scripts.Utils;
using UnityEngine;

namespace Scripts.Interactables
{
    public class OptionalTipZone : TipZone
    {
        [SerializeField, LeanTranslationName] private string _mobileTipToken;

        protected override void ShowTip()
        {
            if (MobileDetector.IsMobile())
            {
                Typewriter.ShowText(GetTipMessage(_mobileTipToken));
            }
            else
            {
                Typewriter.ShowText(GetTipMessage(TipToken));
            }
        }
    }
}