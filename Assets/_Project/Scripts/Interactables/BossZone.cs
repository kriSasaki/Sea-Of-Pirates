using Project.Players.Logic;
using Project.Systems.Cameras;
using UnityEngine;
using Zenject;

namespace Project.Interactables
{
    public class BossZone : InteractableZone
    {
        private CameraSystem _cameraSystem;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player _))
                _cameraSystem.GoToTarget(transform);
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player _))
                _cameraSystem.GoToPlayer();
        }

        [Inject]
        public void Construct(CameraSystem transitionService)
        {
            _cameraSystem = transitionService;
        }
    }
}