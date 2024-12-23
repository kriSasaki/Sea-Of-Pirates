using UnityEngine;

namespace Project.Configs.GameResources
{
    [CreateAssetMenu(fileName = "GameResource", menuName = "Configs/GameResource")]
    public class GameResource : ScriptableObject
    {
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
    }
}