using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Configs.ShopItems
{
    [CreateAssetMenu(fileName = "BundleItem", menuName = "Configs/Shop/InApp/BundleItem")]
    public class BundleItemConfig : InAppItemConfig
    {
        [SerializeField] private List<InAppItemConfig> _items;

        public IReadOnlyList<InAppItemConfig> Items => _items;
    }
}