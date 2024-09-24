using Project.Configs.Level;
using UnityEngine;
using Zenject;

namespace Project.Utils
{
    public class Sea : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _waterRenderer;

        private readonly int _offsetProperty = Shader.PropertyToID("_SurfaceDistortion");
        private readonly float _timeMultiplier = 0.002f;

        private Material _material;

        private void Update()
        {
            float offsetX = Mathf.Repeat(Time.time * _timeMultiplier, 1f);
            Vector2 offset = new Vector2(offsetX, 0f);

            _material.SetTextureOffset(_offsetProperty, offset);
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