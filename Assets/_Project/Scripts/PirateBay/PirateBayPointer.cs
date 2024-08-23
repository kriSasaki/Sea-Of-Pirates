using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PirateBayPointer : MonoBehaviour
{
    [SerializeField] private Transform _pirateBayTransform;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _pointerIconTransform;


    private void Update()
    {
        Vector3 distanceBetweenPirateBayPlayer = transform.position - _pirateBayTransform.position;
        Ray ray = new Ray(_pirateBayTransform.position, distanceBetweenPirateBayPlayer);

        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(_camera);

        float minDistance = Mathf.Infinity;
        int planeIndex = 0;

        for (int i = 0; i < 4; i++)
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

        minDistance = Mathf.Clamp(minDistance, 0.0f, distanceBetweenPirateBayPlayer.magnitude);
        Vector3 worldPosition = ray.GetPoint(minDistance);
        _pointerIconTransform.position = _camera.WorldToScreenPoint(worldPosition);
        _pointerIconTransform.rotation = GetIconRotation(planeIndex);
    }

    Quaternion GetIconRotation(int planeIndex)
    {
        if (planeIndex == 0)
        {
            return Quaternion.Euler(0, 0, 90);
        }
        else if (planeIndex == 1)
        {
            return Quaternion.Euler(0, 0, -90);
        }
        else if (planeIndex == 2)
        {
            return Quaternion.Euler(0, 0, 180);
        }
        else if (planeIndex == 3)
        {
            return Quaternion.Euler(0, 0, 0);
        }

        return Quaternion.identity;
    }
}
