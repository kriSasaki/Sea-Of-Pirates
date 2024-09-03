using Project.Utils.Extensions;
using UnityEngine;

[RequireComponent(typeof(Canvas))]
public class CameraLook : MonoBehaviour
{
    private Canvas _canvas;
    private Camera _camera;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _camera = Camera.main;
        _canvas.worldCamera = _camera;
    }

    private void LateUpdate()
    {
        var rotation = transform.rotation.eulerAngles.WithY(_camera.transform.rotation.eulerAngles.y);
        transform.rotation = _camera.transform.rotation;
    }
}
