using UnityEngine;

namespace Project.Configs.UI
{
    [CreateAssetMenu(fileName = "UiConfigs", menuName = "Configs/UI/UiConfigs")]
    public class UiConfigs : ScriptableObject
    {
        [field: SerializeField] public ItemViewColorConfig GameItemViewColor { get; private set; }
        [field: SerializeField] public ItemViewColorConfig InApptemViewColor { get; private set; }

        [field: SerializeField] public AudioClip OpenWindowSound { get; private set; }
        [field: SerializeField] public AudioClip CloseWindowSound { get; private set; }
        [field: SerializeField] public AudioClip ShowButtonSound { get; private set; }
        [field: SerializeField] public AudioClip RewardOfferSound { get; private set; }
        [field: SerializeField] public AudioClip UpgradeSound { get; private set; }
    }
}