using System;
using System.Collections.Generic;
using Cinemachine;
using Cysharp.Threading.Tasks;
using Project.Configs.Game;
using Project.Players.Logic;
using UnityEngine;
using Zenject;

namespace Project.Systems.Cameras
{
    public class CameraSystem : MonoBehaviour
    {
        [SerializeField] private float _openingViewDuration = 2f;
        [SerializeField] private CinemachineVirtualCamera _playerCamera;
        [SerializeField] private CinemachineVirtualCamera _targetCamera;
        [SerializeField] private CinemachineVirtualCamera _openingCamera;

        private List<CinemachineVirtualCamera> _cameras;
        private CinemachineBrain _brain;
        private Player _player;
        private CinemachineTransposer _targetCameraTransposer;
        private Vector3 _followOffset;

        [Inject]
        public void Construct(Player player, CinemachineBrain brain)
        {
            _player = player;
            _brain = brain;

            _cameras = new List<CinemachineVirtualCamera>()
            {
                _playerCamera,
                _targetCamera,
                _openingCamera,
            };

            _targetCameraTransposer = _targetCamera.GetCinemachineComponent<CinemachineTransposer>();
            _followOffset = _targetCameraTransposer.m_FollowOffset;

            SetPlayerCamera();
            EnableCamera(_openingCamera);
        }

        public async UniTask ShowOpeningAsync()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_openingViewDuration));
            GoToPlayer();
            await UniTask.WaitUntil(() => _brain.IsBlending == false);
        }

        public void GoToTarget(Transform target, CameraFollowOffset cameraFollowOffset = null )
        {
            _targetCameraTransposer.m_FollowOffset = cameraFollowOffset == null ? _followOffset : cameraFollowOffset.Value; 

            SetTargetCamera(target);
            EnableCamera(_targetCamera);
        }

        public void GoToPlayer()
        {
            EnableCamera(_playerCamera);
        }

        public async UniTaskVoid ShowTarget(Transform target, float duration)
        {
            _player.DisableMove();
            GoToTarget(target);
            await UniTask.Delay(TimeSpan.FromSeconds(duration), cancellationToken: destroyCancellationToken);
            GoToPlayer();
            _player.EnableMove();
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

        private void EnableCamera(CinemachineVirtualCamera camera)
        {
            foreach (var virtualCamera in _cameras)
            {
                if (virtualCamera == camera)
                    virtualCamera.gameObject.SetActive(true);
                else
                    virtualCamera.gameObject.SetActive(false);
            }
        }
    }
}