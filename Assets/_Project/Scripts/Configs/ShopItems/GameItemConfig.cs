using Project.Systems.Data;
using UnityEngine;

namespace Project.Configs.ShopItems
{
    public abstract class GameItemConfig : ShopItemConfig
    {
        [field: SerializeField] public GameResourceAmount Price { get; private set; }
    }
}