using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

namespace Scripts.Configs.GameResources
{
    [CreateAssetMenu(fileName = "GameResourcesShert", menuName = "Configs/GameResourceSheet")]
    public class GameResourcesSheet : ScriptableObject
    {
        [SerializeField, Expandable] private List<GameResource> _gameResources;

        public IEnumerable<GameResource> GameResources => _gameResources;
    }
}