using Project.Interactables;
using Project.Players.Logic;
using UnityEngine;
using Zenject;

namespace Project.PirateBays
{
    public class PirateBayPointer : MonoBehaviour
    {
        [SerializeField] private Transform _pointerIconTransform;

        private Canvas _pointerCanvas;
        private Transform _playerTransform;
        private Transform _pirateBayTransform;
        private Camera _camera;
        private float _screenMarginX = 0.2f;
        private float _screenMarginY = 0.2f;
        private float _speedPointerIcon = 10f;

        private Quaternion[] rotations = new Quaternion[]
        {
        Quaternion.Euler(0f, 0f, 90f),
        Quaternion.Euler(0f, 0f, -90f),
        Quaternion.Euler(0f, 0f, 180f),
        Quaternion.Euler(0f, 0f, 0f),
        Quaternion.identity
        };

        private void Awake()
        {
            _pointerCanvas = GetComponent<Canvas>();
        }

        private void Update()
        {
            Vector3 directionToBay = _pirateBayTransform.position - _playerTransform.position;
            Ray ray = new Ray(_playerTransform.position, directionToBay);
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);
            float minDistance = Mathf.Infinity;
            int planeIndex = 0;

            if (!IsVisibleFrom(_camera, _pirateBayTransform))
            {
                _pointerCanvas.enabled = true;
            }
            else
            {
                _pointerCanvas.enabled = false;
            }

            for (int i = 0; i < planes.Length; i++)
            {
                if (planes[i].Raycast(ray, out float distance))
                {
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        planeIndex = i;
                    }
                }
            }

            minDistance = Mathf.Clamp(minDistance, 0.0f, directionToBay.magnitude);
            Vector3 worldPosition = ray.GetPoint(minDistance);
            Vector3 screenPosition = _camera.WorldToScreenPoint(worldPosition);
            float screenWidth = Screen.width;
            float screenHeight = Screen.height;
            screenPosition.x = Mathf.Clamp(screenPosition.x, screenWidth * _screenMarginX, screenWidth * (1 - _screenMarginX));
            screenPosition.y = Mathf.Clamp(screenPosition.y, screenHeight * _screenMarginY, screenHeight * (1 - _screenMarginY));
            _pointerIconTransform.position = Vector3.Lerp(_pointerIconTransform.position, screenPosition, Time.deltaTime * _speedPointerIcon);
            _pointerIconTransform.rotation = Quaternion.Slerp(_pointerIconTransform.rotation, GetIconRotation(planeIndex), Time.deltaTime * _speedPointerIcon);
        }

        [Inject]
        private void Construct(Player player, PirateBay pirateBay)
        {
            _playerTransform = player.transform;
            _pirateBayTransform = pirateBay.transform;
            _camera = Camera.main;
        }

        private Quaternion GetIconRotation(int planeIndex)
        {
            if (planeIndex < 0 || planeIndex >= rotations.Length)
            {
                return rotations[rotations.Length];
            }
            return rotations[planeIndex];
        }

        private bool IsVisibleFrom(Camera camera, Transform transform)
        {
            float pirateBaySizeX = 1f;
            float pirateBaySizeY = 1f;
            float pirateBaySizeZ = 1f;

            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
            Vector3 pirateBaySize = new Vector3(pirateBaySizeX, pirateBaySizeY, pirateBaySizeZ);
            Bounds bounds = new Bounds(transform.position, pirateBaySize);

            return GeometryUtility.TestPlanesAABB(planes, bounds);
        }
    }
}