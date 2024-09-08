﻿using Project.Interactables;
using Project.Players.Logic;
using Project.Systems.Cameras;
using UnityEngine;
using Zenject;

namespace Project.Systems.Upgrades
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