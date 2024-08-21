using NaughtyAttributes;
using UnityEngine;

namespace Project.Configs.GameResources
{
    [CreateAssetMenu(fileName = "GameResource", menuName = "Configs/GameResource")]
    public class GameResource : ScriptableObject
    {
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField, ShowAssetPreview] public Sprite Sprite { get; private set; }
    }
}