using Project.Players.PlayerLogic;
using UnityEngine;

namespace Project.Interactables
{
    public class ZoneUnload : InteractableZone
    {
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.TryGetComponent(out Player player))
            {
                player.UnloadHold();
            }
        }
    }
}