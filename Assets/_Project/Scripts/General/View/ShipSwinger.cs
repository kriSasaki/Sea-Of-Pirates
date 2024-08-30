﻿using Project.Utils.Extensions;
using UnityEngine;

namespace Project.General.View
{
    public class ShipSwinger : MonoBehaviour
    {
        [SerializeField] private float _waterlineLevel = 1f;
        [SerializeField] private float _timeOffsetMultiplier = 2f;
        [SerializeField, Min(0f)] private float _minWaveFrequency = 1f;
        [SerializeField, Min(0.1f)] private float _maxWaveFrequency = 2f;

        private Vector3 _initialPosition;
        private float _timeOffset = 0f;
        private float _waveFrequency = 1f;
        private float _waterlineOffset;

        private void Start()
        {
            _waterlineOffset = (_waterlineLevel + transform.localPosition.y) / 2;
            _initialPosition = transform.localPosition.SubtractY(_waterlineOffset);

            _timeOffset = Random.value * _timeOffsetMultiplier;
            _waveFrequency = Random.Range(_minWaveFrequency, _maxWaveFrequency);
        }

        private void Update()
        {
            transform.localPosition = _initialPosition + _waterlineOffset * Mathf.Sin((Time.time + _timeOffset) * _waveFrequency) * Vector3.down;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(
                transform.position.WithZeroY(),
                transform.position.WithZeroY() + Vector3.forward * 20);


            Gizmos.color = Color.red;

            Gizmos.DrawLine(
                transform.position.AddY(_waterlineLevel),
                transform.position.AddY(_waterlineLevel) + Vector3.forward * 20);
        }
    }
}