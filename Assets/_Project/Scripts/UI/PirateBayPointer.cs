using Scripts.Interactables;
using Scripts.Players.Logic;
using UnityEngine;
using Zenject;

namespace Scripts.UI
{
    public class PirateBayPointer : MonoBehaviour
    {
        [SerializeField] private Transform _pointerTransform;
        [SerializeField, Range(0f, 0.5f)] private float _leftMargin = 0.01f;
        [SerializeField, Range(0f, 0.5f)] private float _rightMargin = 0.1f;
        [SerializeField, Range(0f, 0.5f)] private float _topMargin = 0.1f;
        [SerializeField, Range(0f, 0.5f)] private float _downMargin = 0.01f;

        private Canvas _pointerCanvas;
        private Transform _playerTransform;
        private Transform _pirateBayTransform;
        private Camera _camera;
        private float _pointerSpeed = 10f;

        private Quaternion[] _rotations = new Quaternion[]
        {
            Quaternion.Euler(0f, 0f, 90f),
            Quaternion.Euler(0f, 0f, -90f),
            Quaternion.Euler(0f, 0f, 180f),
            Quaternion.Euler(0f, 0f, 0f),
            Quaternion.identity,
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

            ClampScreenPosition(ref screenPosition);

            float lerpTime = Time.deltaTime * _pointerSpeed;
            Vector3 position = Vector3.Lerp(_pointerTransform.position, screenPosition, lerpTime);
            Quaternion rotation = Quaternion.Slerp(_pointerTransform.rotation, GetIconRotation(planeIndex), lerpTime);

            _pointerTransform.SetPositionAndRotation(position, rotation);
        }

        [Inject]
        private void Construct(Player player, PirateBay pirateBay)
        {
            _playerTransform = player.transform;
            _pirateBayTransform = pirateBay.transform;
            _camera = Camera.main;
        }

        private void ClampScreenPosition(ref Vector3 screenPosition)
        {
            float width = Screen.width;
            float height = Screen.height;
            screenPosition.x = Mathf.Clamp(screenPosition.x, width * _leftMargin, width * (1 - _rightMargin));
            screenPosition.y = Mathf.Clamp(screenPosition.y, height * _downMargin, height * (1 - _topMargin));
        }

        private Quaternion GetIconRotation(int planeIndex)
        {
            return _rotations[planeIndex % _rotations.Length];
        }

        private bool IsVisibleFrom(Camera camera, Transform transform)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
            Vector3 pirateBaySize = Vector3.one;

            Bounds bounds = new Bounds(transform.position, pirateBaySize);

            return GeometryUtility.TestPlanesAABB(planes, bounds);
        }
    }
}