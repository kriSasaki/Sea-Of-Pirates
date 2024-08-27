using UnityEngine;

public class ShipSwinger : MonoBehaviour
{

    [SerializeField] private float _timeOffsetMultiplier = 2f;
    [SerializeField, Min(0f)] private float _minWaveFrequency = 1f;
    [SerializeField, Min(0.1f)] private float _maxWaveFrequency = 2f;

    private Vector3 _initialPosition;
    private float _timeOffset = 0f;
    private float _waterlineOffset = 0.1f;
    private float _waveFrequency = 1f;

    public void Initialize(Vector3 WaterLineOffset)
    {
        _initialPosition = transform.localPosition;
        _waterlineOffset = WaterLineOffset.y - _initialPosition.y;
        _timeOffset = Random.value * _timeOffsetMultiplier;
        _waveFrequency = Random.Range(_minWaveFrequency, _maxWaveFrequency);
    }

    private void Update()
    {
        transform.localPosition = _initialPosition + _waterlineOffset * Mathf.Sin((Time.time + _timeOffset) * _waveFrequency) * Vector3.up;
    }
}