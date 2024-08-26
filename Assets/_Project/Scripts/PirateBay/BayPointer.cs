using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TargetPointer : MonoBehaviour
{

    [SerializeField] Transform _target;
    [SerializeField] RectTransform _pointerUI;
    [SerializeField] Sprite _pointerIcon;
    [SerializeField] Sprite _outOfScreenIcon;
    [SerializeField] float _interfaceScale = 100;

    private Vector3 _startPointerSize;
    private Camera _mainCamera;


    private void Awake()
    {
        _startPointerSize = _pointerUI.sizeDelta;
        _mainCamera = Camera.main;
    }
    private void LateUpdate()
    {
        Vector3 realPos = _mainCamera.WorldToScreenPoint(_target.position);
        Rect rect = new Rect(0, 0, Screen.width, Screen.height);

        Vector3 outPos = realPos;
        float direction = 1;

        _pointerUI.GetComponent<Image>().sprite = _outOfScreenIcon;

        if (!IsBehind(_target.position)) 
        {
            if (rect.Contains(realPos)) // и если цель в окне экрана
            {
                _pointerUI.GetComponent<Image>().sprite = _pointerIcon;
            }
        }
        else // если цель cзади
        {
            realPos = -realPos;
            outPos = new Vector3(realPos.x, 0, 0); // позиция иконки - снизу
            if (_mainCamera.transform.position.y < _target.position.y)
            {
                direction = -1;
                outPos.y = Screen.height; // позиция иконки - сверху				
            }
        }
        // ограничиваем позицию областью экрана
        float offset = _pointerUI.sizeDelta.x / 2;
        outPos.x = Mathf.Clamp(outPos.x, offset, Screen.width - offset);
        outPos.y = Mathf.Clamp(outPos.y, offset, Screen.height - offset);

        Vector3 pos = realPos - outPos; // направление к цели из PointerUI 

        RotatePointer(direction * pos);

        _pointerUI.sizeDelta = new Vector2(_startPointerSize.x / 100 * _interfaceScale, _startPointerSize.y / 100 * _interfaceScale);
        _pointerUI.anchoredPosition = outPos;
    }
    private bool IsBehind(Vector3 point) // true если point сзади камеры
    {
        Vector3 forward = _mainCamera.transform.TransformDirection(Vector3.forward);
        Vector3 toOther = point - _mainCamera.transform.position;
        if (Vector3.Dot(forward, toOther) < 0) return true;
        return false;
    }
    private void RotatePointer(Vector2 direction) // поворачивает PointerUI в направление direction
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        _pointerUI.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}