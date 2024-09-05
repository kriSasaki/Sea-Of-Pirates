using Project.Configs.Level;
using UnityEngine;
using Zenject;

namespace Project.Utils
{
    public class Sea : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _waterRenderer;

        [Inject]
        private void Construct(LevelConfig levelConfig)
        {
            if (_waterRenderer.material == levelConfig.WaterMaterial)
                return;

            _waterRenderer.material = levelConfig.WaterMaterial;
        }
    }
}