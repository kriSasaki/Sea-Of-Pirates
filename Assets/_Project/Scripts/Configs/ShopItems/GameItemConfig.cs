using Project.Systems.Stats;
using UnityEngine;

namespace Project.Configs.ShopItems
{
    public abstract class GameItemConfig : ShopItemConfig
    {
        [field: SerializeField] public GameResourceAmount Price { get; private set; }
    }
}