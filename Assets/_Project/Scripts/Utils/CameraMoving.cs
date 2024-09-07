using System.Collections;
using UnityEngine;
using Cinemachine;

namespace Project.Utils
{
    public class CameraMoving : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera _virtualCamera;
        [SerializeField] private Transform _playerTransform;
        [SerializeField] private float _transitionSpeed = 1f;
        [SerializeField] private float _returnDelay = 1.5f;

        private Vector3 _originalPosition;
        private Quaternion _originalRotation;

        private void Start()
        {
            _originalPosition = _virtualCamera.transform.position;
            _originalRotation = _virtualCamera.transform.rotation;
        }

        public void MoveCameraToTarget(Transform target)
        {
            StartCoroutine(MoveCameraCoroutine(target));
        }

        private IEnumerator MoveCameraCoroutine(Transform target)
        {
            Vector3 targetPosition = target.position;
            Quaternion targetRotation = Quaternion.LookRotation(target.position - _playerTransform.position);
            float elapsedTime = 0f;
            while (elapsedTime < _transitionSpeed)
            {
                float time = elapsedTime / _transitionSpeed;
                _virtualCamera.transform.position = Vector3.Lerp(_virtualCamera.transform.position, targetPosition, time);
                _virtualCamera.transform.rotation = Quaternion.Slerp(_virtualCamera.transform.rotation, targetRotation, time);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _virtualCamera.transform.position = targetPosition;
            _virtualCamera.transform.rotation = targetRotation;

            yield return new WaitForSeconds(_returnDelay);

            elapsedTime = 0f;

            while (elapsedTime < _transitionSpeed)
            {
                float time = elapsedTime / _transitionSpeed;
                _virtualCamera.transform.position = Vector3.Lerp(targetPosition, _originalPosition, time);
                _virtualCamera.transform.rotation = Quaternion.Slerp(targetRotation, _originalRotation, time);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            _virtualCamera.transform.position = _originalPosition;
            _virtualCamera.transform.rotation = _originalRotation;
        }
    }
}
