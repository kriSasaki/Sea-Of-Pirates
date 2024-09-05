using System;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Project.Players.Logic;
using UnityEngine;
using Zenject;

namespace Project.Systems.Cameras
{
    public class CameraSystem : MonoBehaviour
    {
        private const int MaxPriority = 10;
        private const int MinPriority = 0;

        [SerializeField] private CinemachineVirtualCamera _playerCamera;
        [SerializeField] private CinemachineVirtualCamera _targetCamera;

        private Player _player;

        private void Start()
        {
            GoToPlayer();
        }

        [Inject]
        private void Construct(Player player)
        {
            _player = player;

            SetPlayerCamera();
            DisableCamera(_targetCamera);
        }

        private void SetPlayerCamera()
        {
            _playerCamera.Follow = _player.transform;
            _playerCamera.LookAt = _player.transform;
        }

        private void SetTargetCamera(Transform target)
        {
            _targetCamera.Follow = target;
            _targetCamera.LookAt = target;
        }

        public void GoToTarget(Transform target)
        {
            SetTargetCamera(target);
            EnableCamera(_targetCamera);
            DisableCamera(_playerCamera);
        }

        public void GoToPlayer()
        {
            EnableCamera(_playerCamera);
            DisableCamera(_targetCamera);
        }

        public async UniTaskVoid ShowTarget(Transform target, float duration)
        {
            _player.DisableMove();
            GoToTarget(target);
            await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: destroyCancellationToken);
            GoToPlayer();
            _player.EnableMove();
        }

        private void EnableCamera(CinemachineVirtualCamera camera)
        {
            camera.Priority = MaxPriority;
        }

        private void DisableCamera(CinemachineVirtualCamera camera)
        {
            camera.Priority = MinPriority;
        }
    }
}