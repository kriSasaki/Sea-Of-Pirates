using System.Collections.Generic;
using UnityEngine;

namespace Project.Configs.ShopItems
{
    [CreateAssetMenu(fileName = "BundleItem", menuName = "Configs/Shop/InApp/BundleItem")]
    public class BundleItemConfig : InAppItemConfig
    {
        [SerializeField] private List<InAppItemConfig> _items;
        public IReadOnlyList<InAppItemConfig> Items => _items;
    }
}