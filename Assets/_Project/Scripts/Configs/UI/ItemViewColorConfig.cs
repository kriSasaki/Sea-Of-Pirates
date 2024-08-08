using UnityEngine;

namespace Project.Configs.UI
{
    [CreateAssetMenu(fileName = "ItemViewColor", menuName = "Configs/UI/ItemViewColor")]
    public class ItemViewColorConfig : ScriptableObject
    {
        [field: SerializeField] public Color CoverColor { get; private set; }
        [field: SerializeField] public Color BackgroundColor { get; private set; }
    }
}