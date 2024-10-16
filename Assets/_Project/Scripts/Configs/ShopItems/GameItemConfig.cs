using Scripts.Systems.Data;
using UnityEngine;

namespace Scripts.Configs.ShopItems
{
    public abstract class GameItemConfig : ShopItemConfig
    {
        [field: SerializeField] public GameResourceAmount Price { get; private set; }
    }
}