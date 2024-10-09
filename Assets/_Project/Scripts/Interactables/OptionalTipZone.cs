using Lean.Localization;
using Project.Utils;
using UnityEngine;

namespace Project.Interactables
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