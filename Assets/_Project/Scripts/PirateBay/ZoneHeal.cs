using Project.Enemies;
using UnityEngine;

namespace Project.Systems.Interactables
{
    public class ZoneHeal : InteractableZone
    {
        private void OnTriggerEnter(Collider collider)
        {
            if (collider.gameObject.TryGetComponent<Player>(out Player playerComponent))
            {
                Player player = collider.GetComponent<Player>();
                if (player != null)
                {
                    player.RestoreHealthMaximum();
                }
            }
        }
    }
}
