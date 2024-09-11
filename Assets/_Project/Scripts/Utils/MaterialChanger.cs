using NaughtyAttributes;
using Project.Configs.Level;
using UnityEngine;
using Zenject;

public class MaterialChanger : MonoBehaviour
{
    [InfoBox ("Только для смены материалов в Editor")]
    [SerializeField] private Material _material;

    private void Awake()
    {
        ChangeMaterial(transform);
    }

    [Inject]
    private void Construct(LevelConfig config)
    {
        _material = config.LevelMaterial;
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
        if (_material == null || transform == null)
            return;

        ChangeMaterial(transform);
    }
}