using Project.Players.Logic;
using System;
using UnityEngine;

namespace Project.Interactables
{
    public class PirateBay : InteractableZone
    {
        [field: SerializeField] public Transform PlayerRessurectPoint { get; private set; }

        public event Action ReachedByPlayer;

        protected override void OnPlayerEntered(Player player)
        {
            base.OnPlayerEntered(player);

            ReachedByPlayer?.Invoke();
        }
    }
}