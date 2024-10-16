using Scripts.Systems.Data;
using UnityEngine;

namespace Scripts.Configs.ShopItems
{
    [CreateAssetMenu(fileName = "GameResourceItem", menuName = "Configs/Shop/GameResourceItem")]

    public class GameResourceItemConfig : GameItemConfig
    {
        [field: SerializeField] public GameResourceAmount Item { get; private set; }

        public override Sprite Sprite => Item.Resource.Sprite;
    }
}