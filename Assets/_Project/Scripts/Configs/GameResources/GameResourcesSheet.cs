using System.Collections.Generic;
using UnityEngine;

namespace Project.Configs.GameResources
{
    [CreateAssetMenu(fileName = "GameResourcesShert", menuName = "Configs/GameResourceSheet")]
    public class GameResourcesSheet : ScriptableObject
    {
        [SerializeField] private List<GameResource> _gameResources;

        public IEnumerable<GameResource> GameResources => _gameResources;
    }
}