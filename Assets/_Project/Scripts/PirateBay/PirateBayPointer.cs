using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateBayPointer : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform; // Ссылка на объект Player
    [SerializeField] private Camera _camera; // Ссылка на камеру, с которой идет обзор
    [SerializeField] private Transform _pointerIconTransform; // Ссылка на иконку указателя

    // Пределы экрана для указателя (в процентах от ширины и высоты экрана)
    [SerializeField] private float _screenMarginX = 0.1f; // Горизонтальные пределы
    [SerializeField] private float _screenMarginY = 0.1f; // Вертикальные пределы

    private Quaternion[] rotations = new Quaternion[]
    {
    Quaternion.Euler(0f, 0f, 90f),    // вверх
    Quaternion.Euler(0f, 0f, -90f),   // вниз
    Quaternion.Euler(0f, 0f, 180f),   // влево
    Quaternion.Euler(0f, 0f, 0f),     // вправо
    Quaternion.identity                // по умолчанию (в случае незапланированного индекса)
    };

    private void Update()
    {
        Vector3 formPlayerToEnemy = transform.position - _playerTransform.position;
        Ray ray = new Ray(_playerTransform.position, formPlayerToEnemy);

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);
        float minDistance = Mathf.Infinity;
        int planeIndex = 0;

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

        // Ограничиваем указатель в пределах заданных границ экрана
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Вычисляем новые координаты с учетом пределов
        screenPosition.x = Mathf.Clamp(screenPosition.x, screenWidth * _screenMarginX, screenWidth * (1 - _screenMarginX));
        screenPosition.y = Mathf.Clamp(screenPosition.y, screenHeight * _screenMarginY, screenHeight * (1 - _screenMarginY));

        _pointerIconTransform.position = screenPosition;
        _pointerIconTransform.rotation = GetIconRotation(planeIndex);
    }

    // Метод для получения необходимой ротации указателя в зависимости от плоскости
    Quaternion GetIconRotation(int planeIndex)
    {
        if (planeIndex < 0 || planeIndex >= rotations.Length)
        {
            return rotations[4]; // Возвращаем значение по умолчанию, если индекс выходит за пределы
        }

        return rotations[planeIndex];
    }
}
