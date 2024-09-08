using Project.Players.Logic;
using Project.Systems.Upgrades;
using UnityEngine;

namespace Project.Interactables
{
    public class ZoneHeal : InteractableZone
    {
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out Player player))
            {
                player.Heal();
            }
        }
        protected override void OnPlayerEntered(Player player)
        {
            base.OnPlayerEntered(player);

            player.Heal();
        }
    }
}