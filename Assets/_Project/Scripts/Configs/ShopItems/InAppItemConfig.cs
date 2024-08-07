using UnityEngine;

namespace Project.Configs.ShopItems
{
    public abstract class InAppItemConfig : ShopItemConfig
    {
        [SerializeField] private Sprite _sprite;

        public string ID { get; private set; }
        public override Sprite Sprite => _sprite;
    }
}