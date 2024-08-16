using UnityEngine;

namespace Project.Configs.Game
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameCongif")]
    public class GameConfig : ScriptableObject
    {
        [field:SerializeField] public AudioClip EarnResourceSound { get; private set; }
        [field:SerializeField] public AudioClip QuestDoneSound { get; private set; }
    }
}