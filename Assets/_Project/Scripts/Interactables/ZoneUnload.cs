using Project.Players.Logic;
using Project.Systems.Upgrades;
using UnityEngine;

namespace Project.Interactables
{
    public class ZoneUnload : InteractableZone
    {
        protected override void OnPlayerEntered(Player player)
        {
            base.OnPlayerEntered(player);

            player.UnloadHold();
        }
    }
}