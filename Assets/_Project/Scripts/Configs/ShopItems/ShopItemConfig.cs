using UnityEngine;

namespace Scripts.Configs.ShopItems
{
    public abstract class ShopItemConfig : ScriptableObject
    {
        public abstract Sprite Sprite { get; }
    }
}