using Scripts.Players.Logic;
using Scripts.Systems.Upgrades;
using UnityEngine;
using Zenject;

namespace Scripts.Interactables
{
    [RequireComponent(typeof(SphereCollider))]
    public class UpgradePlace : InteractableZone
    {
        private UpgradeSystem _upgradeSystem;

        protected override void OnPlayerEntered(Player player)
        {
            base.OnPlayerEntered(player);

            _upgradeSystem.Show();
        }

        protected override void OnPlayerExited(Player player)
        {
            base.OnPlayerExited(player);

            _upgradeSystem.Hide();
        }

        [Inject]
        private void Construct(UpgradeSystem upgradeSystemView)
        {
            _upgradeSystem = upgradeSystemView;
        }
    }
}