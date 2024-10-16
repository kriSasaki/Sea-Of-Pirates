using Febucci.UI;
using Lean.Localization;
using Scripts.Players.Logic;
using TMPro;
using UnityEngine;

namespace Scripts.Interactables
{
    public class TipZone : InteractableZone
    {
        [SerializeField] private TMP_Text _tip;
        [SerializeField, LeanTranslationName] private string _tipToken;
        [SerializeField] private TypewriterByCharacter _typewriter;

        protected TypewriterByCharacter Typewriter => _typewriter;
        protected string TipToken => _tipToken;

        private void Start()
        {
            _tip.enabled = false;
        }

        protected virtual void ShowTip()
        {
            _typewriter.ShowText(GetTipMessage(_tipToken));
        }

        protected override void OnPlayerEntered(Player player)
        {
            base.OnPlayerEntered(player);
            _tip.enabled = true;

            ShowTip();
        }

        protected override void OnPlayerExited(Player player)
        {
            base.OnPlayerExited(player);

            _typewriter.StartDisappearingText();
            _typewriter.onTextDisappeared.AddListener(() => Destroy(gameObject));
        }

        protected string GetTipMessage(string token)
        {
            return LeanLocalization.GetTranslationText(token);
        }
    }
}