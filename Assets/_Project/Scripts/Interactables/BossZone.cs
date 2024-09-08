using Project.Players.Logic;
using Project.Systems.Cameras;
using Zenject;

namespace Project.Interactables
{
    public class BossZone : InteractableZone
    {
        private CameraSystem _cameraSystem;

        protected override void OnPlayerEntered(Player player)
        {
            base.OnPlayerEntered(player);

            _cameraSystem.GoToTarget(transform);
        }

        protected override void OnPlayerExited(Player player)
        {
            base.OnPlayerExited(player);

            _cameraSystem.GoToPlayer();
        }

        [Inject]
        public void Construct(CameraSystem transitionService)
        {
            _cameraSystem = transitionService;
        }
    }
}