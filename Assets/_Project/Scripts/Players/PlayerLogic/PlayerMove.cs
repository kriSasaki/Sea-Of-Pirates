using Project.Interfaces.Stats;
using Project.Players.CamaraLogic;
using UnityEngine;
using Zenject;

namespace Project.Players.PlayerLogic
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody _playerRigidbody;
        private IInputService _inputService;
        private Camera _camera;
        private Player _player;
        private IPlayerStats _playerStats;

        private int MovementSpeed => _playerStats.Speed;

        private void Start()
        {
            _camera = Camera.main;
        }

        [Inject]
        public void Construct(IPlayerStats playerStats, IInputService inputService)
        {
            _playerStats = playerStats;
            _inputService = inputService;
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > 0.001f)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                transform.forward = movementVector;
            }
            movementVector += Physics.gravity;

            _playerRigidbody.velocity = movementVector * MovementSpeed;
        }

        private void CameraFollow() => _camera.GetComponent<CameraFollow>().Follow(gameObject);
    }
}
