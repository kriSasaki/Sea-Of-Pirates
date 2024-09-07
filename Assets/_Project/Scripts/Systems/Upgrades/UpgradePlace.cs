using Project.Interactables;
using Project.Players.Logic;
using UnityEngine;
using Zenject;

namespace Project.Systems.Upgrades
{
    [RequireComponent(typeof(SphereCollider))]
    public class UpgradePlace : InteractableZone
    {
        private UpgradeSystem _upgradeSystem;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _upgradeSystem.Show();
                OnPlayerEntered();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _upgradeSystem.Hide();
                OnPlayerCameOut();
            }
        }
 
        [Inject]
        private void Construct(UpgradeSystem upgradeSystemView)
        {
           _upgradeSystem = upgradeSystemView;
        }
    }
}