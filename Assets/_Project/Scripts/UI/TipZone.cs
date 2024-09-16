using Febucci.UI;
using Lean.Localization;
using Project.Players.Logic;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Project.Interactables
{
    public class TipZone : InteractableZone
    {
        [SerializeField] private TMP_Text _tip;
        [SerializeField, LeanTranslationName] private string _tipToken;
        [SerializeField] private TypewriterByCharacter _typewriter;

        protected string TipToken => _tipToken;
        protected TypewriterByCharacter Typewriter => _typewriter;

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
        }

        protected string GetTipMessage(string token)
        {
            return LeanLocalization.GetTranslationText(token);
        }
    }
}