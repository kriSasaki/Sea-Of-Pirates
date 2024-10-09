using Project.Configs.Game;
using Project.Players.Logic;
using Project.Systems.Cameras;
using UnityEngine;
using Zenject;

namespace Project.Interactables
{
    public class CameraViewZone : InteractableZone
    {
        [SerializeField] private CameraFollowOffset _followOffset;

        private CameraSystem _cameraSystem;

        [Inject]
        public void Construct(CameraSystem transitionService)
        {
            _cameraSystem = transitionService;
        }

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
    }
}