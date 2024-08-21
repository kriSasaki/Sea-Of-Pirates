using Project.Systems.Stats;
using UnityEngine;

namespace Project.Configs.ShopItems
{
    [CreateAssetMenu(fileName = "InAppResourceItem", menuName = "Configs/Shop/InApp/InAppResource")]
    public class InAppResourceItemConfig : InAppItemConfig
    {
        [field: SerializeField] public GameResourceAmount Item { get; private set; }
    }
}