using NaughtyAttributes;
using UnityEngine;

namespace Project.Configs.Level
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [SerializeField, Scene] private string _nextLevelScene;

        [field: SerializeField] public bool IsLastLevel { get; private set; }
        [field: SerializeField] public Material LevelMaterial { get; private set; }
        [field: SerializeField] public Material WaterMaterial { get; private set; }

        public string NextLevel => _nextLevelScene;
    }
}