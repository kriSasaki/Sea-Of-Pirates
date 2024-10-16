using System;
using Scripts.Players.Logic;
using UnityEngine;

namespace Scripts.Interactables
{
    public class PirateBay : InteractableZone
    {
        public event Action ReachedByPlayer;

        [field: SerializeField] public Transform PlayerRessurectPoint { get; private set; }

        protected override void OnPlayerEntered(Player player)
        {
            base.OnPlayerEntered(player);

            ReachedByPlayer?.Invoke();
        }
    }
}