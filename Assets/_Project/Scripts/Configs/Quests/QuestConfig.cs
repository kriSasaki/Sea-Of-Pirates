using Lean.Localization;
using Scripts.Configs.Enemies;
using Scripts.Systems.Data;
using UnityEngine;

namespace Scripts.Configs.Quests
{
    [CreateAssetMenu(fileName = "Quest", menuName = "Configs/Quest")]
    public class QuestConfig : ScriptableObject
    {
        private const string CompleteMessageToken = "quests/CompleteMessage";

        [SerializeField, LeanTranslationName] private string _descriptionToken;

        [field: SerializeField] public EnemyConfig TargetType { get; private set; }
        [field: SerializeField, Min(1)] public int TargetAmount { get; private set; }
        [field: SerializeField] public GameResourceAmount Reward { get; private set; }

        public string ID => name;
        public string Description => LeanLocalization.GetTranslationText(_descriptionToken);
        public string CompleteMessage => LeanLocalization.GetTranslationText(CompleteMessageToken);
    }
}