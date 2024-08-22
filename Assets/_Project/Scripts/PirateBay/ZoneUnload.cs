using UnityEngine;
using Project.Players.Hold;
using Project.Enemies;

namespace Project.Systems.Interactables
{
    public class ZoneUnload : InteractableZone
    {
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.TryGetComponent<Player>(out Player playerComponent))
            {
                Player player = collider.GetComponent<Player>();
                if (player != null)
                {
                    player.LoadPlayerHoldStorage();
                }
            }
        }
    }
}
