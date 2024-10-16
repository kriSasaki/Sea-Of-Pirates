using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Configs.ShopItems
{
    [CreateAssetMenu(fileName = "ShopItemsConfigs", menuName = "Configs/Shop/ItemsConfigs")]

    public class ShopItemsConfigs : ScriptableObject
    {
        [SerializeField] private List<GameItemConfig> _gameItems;
        [SerializeField] private List<InAppItemConfig> _inAppItems;

        public IEnumerable<GameItemConfig> GameItemsConfigs => _gameItems;
        public IEnumerable<InAppItemConfig> InAppItemsConfigs => _inAppItems;
    }
}