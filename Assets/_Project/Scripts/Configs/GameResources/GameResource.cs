using NaughtyAttributes;
using UnityEngine;

namespace Scripts.Configs.GameResources
{
    [CreateAssetMenu(fileName = "GameResource", menuName = "Configs/GameResource")]
    public class GameResource : ScriptableObject
    {
        [field: SerializeField, ShowAssetPreview] public Sprite Sprite { get; private set; }

        public string ID => name;
    }
}