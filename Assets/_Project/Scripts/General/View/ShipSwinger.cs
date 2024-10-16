using Scripts.Utils.Extensions;
using UnityEngine;

namespace Scripts.General.View
{
    public class ShipSwinger : MonoBehaviour
    {
        private const float GizmoLineLenght = 20f;

        [SerializeField] private float _waterlineLevel = 1f;
        [SerializeField] private float _timeOffsetMultiplier = 2f;
        [SerializeField, Min(0f)] private float _minWaveFrequency = 1f;
        [SerializeField, Min(0.1f)] private float _maxWaveFrequency = 2f;

        private Vector3 _initialPosition;
        private float _timeOffset = 0f;
        private float _waveFrequency = 1f;
        private float _waterlineOffset;

        private void Update()
        {
            Vector3 offset = _waterlineOffset * Mathf.Sin((Time.time + _timeOffset) * _waveFrequency) * Vector3.down;
            transform.localPosition = _initialPosition + offset;
        }

        public void Initialize()
        {
            _waterlineOffset = (_waterlineLevel + transform.localPosition.y) / 2;
            _initialPosition = transform.localPosition.SubtractY(_waterlineOffset);

            _timeOffset = Random.value * _timeOffsetMultiplier;
            _waveFrequency = Random.Range(_minWaveFrequency, _maxWaveFrequency);
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(
                transform.position.WithZeroY(),
                transform.position.WithZeroY() + transform.forward * GizmoLineLenght);

            Gizmos.color = Color.red;

            Gizmos.DrawLine(
                transform.position.AddY(_waterlineLevel),
                transform.position.AddY(_waterlineLevel) + transform.forward * GizmoLineLenght);
        }
    }
}