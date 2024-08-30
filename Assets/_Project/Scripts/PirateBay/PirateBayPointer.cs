using UnityEngine;


public class PirateBayPointer : MonoBehaviour
{
    [SerializeField] private Transform _pointerIconTransform;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Canvas _pointerCanvas;

    private float _screenMarginX = 0.2f;
    private float _screenMarginY = 0.2f;
    private Transform _playerTransform;
    private Camera _camera;

    private Quaternion[] rotations = new Quaternion[]
    {
        Quaternion.Euler(0f, 0f, 90f),
        Quaternion.Euler(0f, 0f, -90f),
        Quaternion.Euler(0f, 0f, 180f),
        Quaternion.Euler(0f, 0f, 0f),
        Quaternion.identity
    };

    private void Start()
    {
        _camera = Camera.main;
        _playerTransform = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        Vector3 formPlayerToEnemy = transform.position - _playerTransform.position;
        Ray ray = new Ray(_playerTransform.position, formPlayerToEnemy);

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);
        float minDistance = Mathf.Infinity;
        int planeIndex = 0;
        if (!IsVisibleFrom(_camera, _renderer))
        {
            _pointerCanvas.enabled = true;
        }
        else
        {
            _pointerCanvas.enabled = false;
        }

        for (int i = 0; i < 6; i++)
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

        minDistance = Mathf.Clamp(minDistance, 0.0f, formPlayerToEnemy.magnitude);
        Vector3 worldPosition = ray.GetPoint(minDistance);
        Vector3 screenPosition = _camera.WorldToScreenPoint(worldPosition);

        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        screenPosition.x = Mathf.Clamp(screenPosition.x, screenWidth * _screenMarginX, screenWidth * (1 - _screenMarginX));
        screenPosition.y = Mathf.Clamp(screenPosition.y, screenHeight * _screenMarginY, screenHeight * (1 - _screenMarginY));

        _pointerIconTransform.position = screenPosition;
        _pointerIconTransform.rotation = GetIconRotation(planeIndex);
    }

    Quaternion GetIconRotation(int planeIndex)
    {
        if (planeIndex < 0 || planeIndex >= rotations.Length)
        {
            return rotations[4];
        }

        return rotations[planeIndex];
    }
  
    private bool IsVisibleFrom(Camera cam, Renderer renderer)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}