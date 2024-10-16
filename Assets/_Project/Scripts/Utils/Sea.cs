using Scripts.Configs.Level;
using UnityEngine;
using Zenject;

namespace Scripts.Utils
{
    public class Sea : MonoBehaviour
    {
        private const float MaxOffsetValue = 1f;

        [SerializeField] private MeshRenderer _waterRenderer;

        private readonly int _offsetProperty = Shader.PropertyToID("_SurfaceDistortion");
        private readonly float _timeMultiplier = 0.002f;

        private Material _material;
        private float _offsetValue;

        private void Update()
        {
            _offsetValue = Mathf.Repeat(_offsetValue + Time.deltaTime * _timeMultiplier, MaxOffsetValue);
            Vector2 distortionOffset = new Vector2(_offsetValue, -_offsetValue);

            _material.SetTextureOffset(_offsetProperty, distortionOffset);
        }

        [Inject]
        private void Construct(LevelConfig levelConfig)
        {
            if (_waterRenderer.material == levelConfig.WaterMaterial)
                return;

            _waterRenderer.material = levelConfig.WaterMaterial;
            _material = _waterRenderer.material;
        }
    }
}