using NaughtyAttributes;
using Scripts.Configs.Level;
using Scripts.Installers.SceneContext;
using UnityEngine;
using Zenject;

namespace Scripts.Utils
{
    public class MaterialChanger : MonoBehaviour
    {
        private Material _material;
        private LevelConfig _levelConfig;

        private void Awake()
        {
            _material = _levelConfig.LevelMaterial;

            ChangeMaterial(transform);
        }

        [Inject]
        private void Construct(LevelConfig config)
        {
            _levelConfig = config;
        }

        private void ChangeMaterial(Transform transform)
        {
            if (transform.TryGetComponent<MeshRenderer>(out var renderer))
            {
                renderer.material = _material;
            }

            foreach (Transform child in transform)
            {
                ChangeMaterial(child);
            }
        }

        [ExecuteInEditMode]
        [Button]
        private void ReplaceMaterial()
        {
            var sceneInstaller = FindAnyObjectByType<SceneInstaller>();

            if (sceneInstaller != null)
            {
                _material = sceneInstaller.LevelConfig.LevelMaterial;
                ChangeMaterial(transform);
            }
        }
    }
}