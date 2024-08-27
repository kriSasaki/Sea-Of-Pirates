using Project.Players.Logic;
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
    }
}