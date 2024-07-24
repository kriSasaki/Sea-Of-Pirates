using Project.Players.CamaraLogic;
using UnityEngine;

namespace Project.Players.PlayerLogic
{
    public class PlayerMove : MonoBehaviour
    {
        [SerializeField] private CharacterController CharacterController;
        [SerializeField] private float MovementSpeed;
        private IInputService _inputService;
        private Camera _camera;

        private void Awake()
        {
  //          _inputService = Game.InputService;
        }

        private void Start()
        {
            _camera = Camera.main;
        }

        private void CameraFollow() => _camera.GetComponent<CameraFollow>().Follow(gameObject);

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
            CharacterController.Move(MovementSpeed * movementVector * Time.deltaTime);
        }
    }
}
