using UnityEngine;

namespace Project.Players.PlayerLogic
{
    public class Bars : MonoBehaviour
    {
        [SerializeField] private Transform _scaleTransform;
        [SerializeField] private Transform _target;

        private Transform _cameraTransform;
        private float _xScale;
        [SerializeField] private float _heightOffset = 10.0f;

        private void Start()
        {
            _cameraTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            transform.position = _target.position + Vector3.up * _heightOffset;
            transform.rotation = _cameraTransform.rotation;
        }

        public void Setup(Transform target)
        {
            _target = target;
        }

        public void SetHealth(int stats, int maxStats)
        {
            _xScale = (float)stats / maxStats;
            _xScale = Mathf.Clamp01(_xScale);
        }
    }
}
