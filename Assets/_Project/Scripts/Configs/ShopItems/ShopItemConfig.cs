using UnityEngine;

namespace Project.Configs.ShopItems
{
    public abstract class ShopItemConfig : ScriptableObject
    {
        public abstract Sprite Sprite { get; }
    }
}