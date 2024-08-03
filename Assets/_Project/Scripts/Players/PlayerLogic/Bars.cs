using UnityEngine;

namespace Project.Players.PlayerLogic
{
    public class Bars : MonoBehaviour
    {
        [SerializeField] private Transform _scaleTransform;
        [SerializeField] private Transform _target;

        private Transform _cameraTransform;
        private float _heightHealthBar = 30f;
        private float _xScale;

        private void Start()
        {
            _cameraTransform = Camera.main.transform;
        }

        private void LateUpdate()
        {
            transform.position = _target.position + Vector3.up;
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