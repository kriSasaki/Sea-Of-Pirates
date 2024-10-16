using Scripts.Configs.Game;
using Scripts.Players.Logic;
using Scripts.Systems.Cameras;
using UnityEngine;
using Zenject;

namespace Scripts.Interactables
{
    public class CameraViewZone : InteractableZone
    {
        [SerializeField] private CameraFollowOffset _followOffset;

        private CameraSystem _cameraSystem;

        protected override void OnPlayerEntered(Player player)
        {
            base.OnPlayerEntered(player);

            _cameraSystem.GoToTarget(transform, _followOffset);
        }

        protected override void OnPlayerExited(Player player)
        {
            base.OnPlayerExited(player);

            _cameraSystem.GoToPlayer();
        }

        [Inject]
        private void Construct(CameraSystem transitionService)
        {
            _cameraSystem = transitionService;
        }
    }
}