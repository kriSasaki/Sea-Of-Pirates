using System;
using Project.Players.Logic;
using UnityEngine;

namespace Project.Interactables
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