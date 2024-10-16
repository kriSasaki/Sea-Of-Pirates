using UnityEngine;

namespace Scripts.Configs.ShopItems
{
    public abstract class InAppItemConfig : ShopItemConfig
    {
        [SerializeField] private Sprite _sprite;

        [field:SerializeField] public string ID { get; private set; }

        public override Sprite Sprite => _sprite;
    }
}