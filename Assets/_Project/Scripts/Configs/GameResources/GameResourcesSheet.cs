using System.Collections.Generic;
using UnityEngine;

namespace Project.Configs.GameResources
{
    [CreateAssetMenu(fileName = "GameResourcesShert", menuName = "Configs/GameResourceSheet")]
    public class GameResourcesSheet : ScriptableObject
    {
        [field: SerializeField] public List<GameResource> GameResources { get; private set;}
    }
}